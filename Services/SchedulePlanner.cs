using System;
using System.Collections.Generic;
using System.Linq;
using VideoScheduler.Data;
using VideoScheduler.Data.Models;

namespace VideoScheduler.Services
{
    public class SchedulePlanner
    {
        private readonly YamlRepository _repository;
        private readonly Random _random = new Random();

        public List<PlaylistItem> CurrentPlaylist 
        { 
            get => _repository.AppState.CurrentPlaylist;
            private set => _repository.AppState.CurrentPlaylist = value;
        }
        
        public event Action? OnPlaylistUpdated;

        private class ResolvedBlockItem
        {
            public BlockItem OriginalItem { get; set; } = new();
            public Asset? Asset { get; set; }
            public string SourceInfo { get; set; } = string.Empty;
            public double Duration { get; set; }
            public bool IsAutoCommercial { get; set; }
            public string? RunId { get; set; }
        }

        private class SimRunState
        {
            public string CurrentEpisodeId { get; set; } = string.Empty;
            public Queue<string> ShuffleQueue { get; set; } = new Queue<string>();
        }

        public SchedulePlanner(YamlRepository repository)
        {
            _repository = repository;
        }

        public void Initialize()
        {
            // Validate existing playlist on startup
            if (CurrentPlaylist.Any())
            {
                var now = DateTime.Now;
                // Remove items that ended in the past (keep current item)
                var validItems = CurrentPlaylist.Where(i => i.EstimatedStartTime.AddSeconds(i.DurationSec) > now).ToList();
                
                if (validItems.Count < CurrentPlaylist.Count)
                {
                    CurrentPlaylist = validItems;
                    _repository.SaveAppState();
                }
            }

            RefreshForecast();
        }

        public void RefreshForecast(string? changedRunId = null, bool forceCurrentBlock = false)
        {
            var now = DateTime.Now;
            DateTime safeStartTime = now;
            List<PlaylistItem> keptItems = new();

            if (forceCurrentBlock)
            {
                // Regenerate from NOW (Cut off current item)
                safeStartTime = now;
                // Keep history (items that have already finished)
                if (CurrentPlaylist != null)
                {
                    keptItems = CurrentPlaylist.Where(i => i.EstimatedStartTime.AddSeconds(i.DurationSec) <= now).ToList();
                }
            }
            else
            {
                // Standard Generate: Preserve Current Block
                // 1. Find Current TimeBlock End
                var todaySchedule = _repository.Schedule.Days.FirstOrDefault(d => d.Day == now.DayOfWeek);
                var currentBlock = todaySchedule?.TimeBlocks.FirstOrDefault(tb => 
                    IsTimeInBlock(now.TimeOfDay, tb.StartTime, tb.EndTime));
                
                DateTime cutoffTime;
                if (currentBlock != null)
                {
                    // We are in a block. Preserve everything in this block.
                    // Calculate block end time for today
                    var blockEnd = now.Date.Add(currentBlock.EndTime.ToTimeSpan());
                    if (currentBlock.EndTime <= currentBlock.StartTime) blockEnd = blockEnd.AddDays(1); // Midnight crossing
                    
                    cutoffTime = blockEnd;
                }
                else
                {
                    // We are in a gap. Preserve everything up to now (or up to the next block?)
                    // Let's just preserve up to now to be safe, or maybe the end of the *last* block?
                    // If we are in a gap, "Current Block" is undefined.
                    // But the user wants to preserve "Current Video".
                    // So let's preserve up to the end of the current video at least.
                    
                    var currentItem = CurrentPlaylist?.FirstOrDefault(i => 
                        i.EstimatedStartTime <= now && 
                        i.EstimatedStartTime.AddSeconds(i.DurationSec) > now);
                        
                    cutoffTime = currentItem != null 
                        ? currentItem.EstimatedStartTime.AddSeconds(currentItem.DurationSec) 
                        : now;
                }

                // 2. Keep items
                if (CurrentPlaylist != null)
                {
                    keptItems = CurrentPlaylist.Where(i => i.EstimatedStartTime < cutoffTime).ToList();
                }
                
                // 3. Determine Start Time for New Items
                if (keptItems.Any())
                {
                    var lastKept = keptItems.Last();
                    var lastKeptEnd = lastKept.EstimatedStartTime.AddSeconds(lastKept.DurationSec);
                    safeStartTime = lastKeptEnd > cutoffTime ? lastKeptEnd : cutoffTime;
                }
                else
                {
                    safeStartTime = cutoffTime;
                }
            }

            // 2. Prepare Simulation State
            var simulatedRuns = _repository.Library.Runs.ToDictionary(
                r => r.Id,
                r => new SimRunState
                {
                    CurrentEpisodeId = r.CurrentEpisodeId,
                    ShuffleQueue = new Queue<string>(r.ShuffleQueue)
                });
            
            // Fast-forward simulation through kept items
            foreach (var item in keptItems)
            {
                // Only advance for future items. The current item is already accounted for 
                // in the DB (simulatedRuns) because AutomationService advances it on start.
                bool isCurrent = item.EstimatedStartTime <= now && item.EstimatedStartTime.AddSeconds(item.DurationSec) > now;
                
                if (!isCurrent && !string.IsNullOrEmpty(item.RunId))
                {
                    AdvanceSimulatedRun(item.RunId, simulatedRuns);
                }
            }

            // 3. Generate New Items
            // Generate for configured days
            var daysToGenerate = _repository.AppState.ForecastDays;
            if (daysToGenerate < 1) daysToGenerate = 3;

            var newItems = GenerateForecastInternal(safeStartTime, daysToGenerate, simulatedRuns);
            
            // 4. Merge
            if (keptItems.Any() && newItems.Any())
            {
                var lastKept = keptItems.Last();
                var firstNew = newItems.First();
                
                if (lastKept.AssetId == firstNew.AssetId && firstNew.EstimatedStartTime < lastKept.EstimatedStartTime.AddSeconds(lastKept.DurationSec))
                {
                    newItems.RemoveAt(0);
                }
            }
            CurrentPlaylist = keptItems.Concat(newItems).ToList();
            _repository.SaveAppState();
            OnPlaylistUpdated?.Invoke();
        }

        private bool IsTimeInBlock(TimeSpan time, TimeOnly start, TimeOnly end)
        {
            if (start <= end)
                return time >= start.ToTimeSpan() && time < end.ToTimeSpan();
            else
                return time >= start.ToTimeSpan() || time < end.ToTimeSpan();
        }

        private void AdvanceSimulatedRun(string runId, Dictionary<string, SimRunState> simulatedRuns)
        {
            var run = _repository.Library.Runs.FirstOrDefault(r => r.Id == runId);
            if (run == null) return;

            var show = _repository.Library.Shows.FirstOrDefault(s => s.Id == run.ShowId);
            if (show == null || !show.Episodes.Any()) return;

            if (!simulatedRuns.TryGetValue(runId, out var state)) return;

            var episodes = GetEpisodesInRange(run, show);
            if (!episodes.Any()) return;

            if (run.Randomize)
            {
                if (state.ShuffleQueue.Count == 0)
                {
                    foreach (var id in BuildShuffleQueue(episodes, state.CurrentEpisodeId))
                        state.ShuffleQueue.Enqueue(id);
                }
                state.CurrentEpisodeId = state.ShuffleQueue.Dequeue();
            }
            else
            {
                var episode = episodes.FirstOrDefault(e => e.Id == state.CurrentEpisodeId) ?? episodes.First();
                var nextIndex = (episodes.IndexOf(episode) + 1) % episodes.Count;
                state.CurrentEpisodeId = episodes[nextIndex].Id;
            }
        }

        public void AdvanceRun(string runId)
        {
            var run = _repository.Library.Runs.FirstOrDefault(r => r.Id == runId);
            if (run == null) return;

            var show = _repository.Library.Shows.FirstOrDefault(s => s.Id == run.ShowId);
            if (show == null || !show.Episodes.Any()) return;

            var episodes = GetEpisodesInRange(run, show);
            if (!episodes.Any()) return;

            if (run.Randomize)
            {
                if (run.ShuffleQueue.Count == 0)
                    run.ShuffleQueue = BuildShuffleQueue(episodes, run.CurrentEpisodeId);

                run.CurrentEpisodeId = run.ShuffleQueue[0];
                run.ShuffleQueue.RemoveAt(0);
            }
            else
            {
                var episode = episodes.FirstOrDefault(e => e.Id == run.CurrentEpisodeId) ?? episodes.First();
                var nextIndex = (episodes.IndexOf(episode) + 1) % episodes.Count;
                run.CurrentEpisodeId = episodes[nextIndex].Id;
            }

            _repository.SaveLibrary();
        }

        public List<PlaylistItem> GenerateForecast(DateTime startDate, int daysCount)
        {
            // Legacy wrapper or for manual generation
            var simulatedRuns = _repository.Library.Runs.ToDictionary(
                r => r.Id,
                r => new SimRunState
                {
                    CurrentEpisodeId = r.CurrentEpisodeId,
                    ShuffleQueue = new Queue<string>(r.ShuffleQueue)
                });
            return GenerateForecastInternal(startDate, daysCount, simulatedRuns);
        }

        private List<PlaylistItem> GenerateForecastInternal(DateTime startDate, int daysCount, Dictionary<string, SimRunState> simulatedRuns)
        {
            var playlist = new List<PlaylistItem>();
            
            for (int i = 0; i < daysCount; i++)
            {
                var currentDate = startDate.Date.AddDays(i);
                var dayOfWeek = currentDate.DayOfWeek;
                var daySchedule = _repository.Schedule.Days.FirstOrDefault(d => d.Day == dayOfWeek);

                if (daySchedule == null) continue;

                foreach (var timeBlock in daySchedule.TimeBlocks.OrderBy(tb => tb.StartTime))
                {
                    var blockStartDateTime = currentDate.Add(timeBlock.StartTime.ToTimeSpan());
                    var blockDuration = (timeBlock.EndTime - timeBlock.StartTime).TotalSeconds;
                    if (blockDuration < 0) blockDuration += 24 * 3600; // Handle midnight crossing duration
                    var blockEndDateTime = blockStartDateTime.AddSeconds(blockDuration);
                    
                    // Skip if block ended in the past
                    if (blockEndDateTime <= startDate) continue;

                    var currentTimeCursor = blockStartDateTime;

                    var resolvedItems = new List<ResolvedBlockItem>();
                    var usedCommercialsInBlock = new HashSet<string>();

                    // 1. Resolve Content & Fixed Items
                    foreach (var segment in timeBlock.Segments)
                    {
                        var template = _repository.Templates.Templates.FirstOrDefault(t => t.Id == segment.TemplateId);
                        if (template == null) continue;

                        foreach (var item in template.Structure)
                        {
                            if (item.Type == BlockItemType.CommercialBreak)
                            {
                                resolvedItems.Add(new ResolvedBlockItem 
                                { 
                                    OriginalItem = item, 
                                    IsAutoCommercial = item.AutoDuration,
                                    Duration = item.AutoDuration ? 0 : item.Duration ?? 30
                                });
                            }
                            else
                            {
                                Asset? asset = null;
                                string sourceInfo = "";

                                if (item.Type == BlockItemType.Fixed)
                                {
                                    asset = FindAsset(item.AssetId);
                                    sourceInfo = "Fixed";
                                }
                                else if (item.Type == BlockItemType.RandomFromGroup)
                                {
                                    asset = PickRandomAsset(item);
                                    sourceInfo = item.GroupId != null ? "Dynamic Group" : "Dynamic Category";
                                }
                                else if (item.Type == BlockItemType.ContentSegment)
                                {
                                    asset = GetNextEpisode(item.RunId, simulatedRuns);
                                    var run = _repository.Library.Runs.FirstOrDefault(r => r.Id == item.RunId);
                                    sourceInfo = run != null ? $"Run: {run.Name}" : "Run";
                                }
                                else if (item.Type == BlockItemType.Movie)
                                {
                                    asset = _repository.Library.Movies.FirstOrDefault(m => m.Id == item.MovieId);
                                    sourceInfo = "Movie";
                                }

                                    resolvedItems.Add(new ResolvedBlockItem
                                    {
                                        OriginalItem = item,
                                        Asset = asset,
                                        Duration = asset?.DurationSec ?? 0,
                                        SourceInfo = sourceInfo,
                                        RunId = item.Type == BlockItemType.ContentSegment ? item.RunId : null
                                    });
                            }
                        }
                    }

                    // 2. Calculate Auto Commercial Durations
                    var totalContentDuration = resolvedItems
                        .Where(x => !x.IsAutoCommercial && x.OriginalItem.Type != BlockItemType.CommercialBreak)
                        .Sum(x => x.Duration);
                    
                    var fixedCommercialDuration = resolvedItems
                        .Where(x => !x.IsAutoCommercial && x.OriginalItem.Type == BlockItemType.CommercialBreak)
                        .Sum(x => x.Duration);

                    var remainingTime = blockDuration - totalContentDuration - fixedCommercialDuration;
                    var autoBreaksCount = resolvedItems.Count(x => x.IsAutoCommercial);
                    var autoDuration = autoBreaksCount > 0 ? Math.Max(0, remainingTime / autoBreaksCount) : 0;

                    // 3. Generate Playlist
                    foreach (var item in resolvedItems)
                    {
                        if (item.OriginalItem.Type == BlockItemType.CommercialBreak)
                        {
                            var targetDuration = item.IsAutoCommercial ? autoDuration : item.Duration;
                            var commercials = PickCommercials(item.OriginalItem.GroupId, targetDuration, usedCommercialsInBlock);
                            
                            foreach (var comm in commercials)
                            {
                                if (currentTimeCursor.AddSeconds(comm.DurationSec) > startDate)
                                {
                                    playlist.Add(new PlaylistItem
                                    {
                                        EstimatedStartTime = currentTimeCursor,
                                        Title = comm.Title,
                                        Duration = $"{comm.DurationSec / 60}:{comm.DurationSec % 60:00}",
                                        DurationSec = comm.DurationSec,
                                        Type = "Commercial",
                                        AssetId = comm.Id,
                                        FilePath = comm.FilePath,
                                        Source = item.IsAutoCommercial ? "Auto Commercial" : "Fixed Commercial"
                                    });
                                }
                                currentTimeCursor = currentTimeCursor.AddSeconds(comm.DurationSec);
                            }
                        }
                        else if (item.Asset != null)
                        {
                            if (currentTimeCursor.AddSeconds(item.Duration) > startDate)
                            {
                                playlist.Add(new PlaylistItem
                                {
                                    EstimatedStartTime = currentTimeCursor,
                                    Title = item.Asset.Title,
                                    Duration = $"{item.Asset.DurationSec / 60}:{item.Asset.DurationSec % 60:00}",
                                    DurationSec = item.Asset.DurationSec,
                                    Type = item.Asset.Type.ToString(),
                                    AssetId = item.Asset.Id,
                                    FilePath = item.Asset.FilePath,
                                    Source = item.SourceInfo,
                                    RunId = item.RunId,
                                    EpisodeId = item.Asset.Id
                                });
                            }
                            currentTimeCursor = currentTimeCursor.AddSeconds(item.Duration);
                        }
                    }
                }
            }

            return playlist;
        }

        private List<Asset> PickCommercials(string? groupId, double targetDuration, HashSet<string> usedIds)
        {
            var selected = new List<Asset>();
            var candidates = new List<Asset>();

            if (!string.IsNullOrEmpty(groupId))
            {
                var group = _repository.Library.Groups.FirstOrDefault(g => g.Id == groupId);
                if (group != null)
                {
                    candidates = _repository.Library.Commercials.Where(c => group.AssetIds.Contains(c.Id)).ToList();
                }
            }
            else
            {
                candidates = _repository.Library.Commercials.ToList();
            }

            // Filter used
            var available = candidates.Where(c => !usedIds.Contains(c.Id)).ToList();
            
            // Shuffle
            available = available.OrderBy(x => _random.Next()).ToList();

            double currentDuration = 0;
            foreach (var comm in available)
            {
                if (currentDuration + comm.DurationSec <= targetDuration)
                {
                    selected.Add(comm);
                    usedIds.Add(comm.Id);
                    currentDuration += comm.DurationSec;
                }
            }

            return selected;
        }

        private Asset? FindAsset(string assetId)
        {
            var allAssets = GetAllAssets();
            return allAssets.FirstOrDefault(a => a.Id == assetId);
        }

        private IEnumerable<Asset> GetAllAssets()
        {
            return _repository.Library.Shows.SelectMany(s => s.Episodes).Cast<Asset>()
                .Concat(_repository.Library.Movies)
                .Concat(_repository.Library.Bumpers)
                .Concat(_repository.Library.Commercials);
        }

        private Asset? PickRandomAsset(BlockItem item)
        {
            IEnumerable<Asset> candidates = Enumerable.Empty<Asset>();

            if (!string.IsNullOrEmpty(item.GroupId))
            {
                var group = _repository.Library.Groups.FirstOrDefault(g => g.Id == item.GroupId);
                if (group != null)
                {
                    var allAssets = GetAllAssets();
                    candidates = allAssets.Where(a => group.AssetIds.Contains(a.Id));
                }
            }
            else if (item.Category.HasValue)
            {
                var allAssets = GetAllAssets();
                candidates = allAssets.Where(a => a.Type == item.Category.Value);
            }

            var list = candidates.ToList();
            if (list.Count == 0) return null;
            return list[_random.Next(list.Count)];
        }

        private Asset? GetNextEpisode(string runId, Dictionary<string, SimRunState> simulatedRuns)
        {
            var run = _repository.Library.Runs.FirstOrDefault(r => r.Id == runId);
            if (run == null) return null;

            var show = _repository.Library.Shows.FirstOrDefault(s => s.Id == run.ShowId);
            if (show == null || !show.Episodes.Any()) return null;

            if (!simulatedRuns.TryGetValue(runId, out var state))
            {
                state = new SimRunState
                {
                    CurrentEpisodeId = run.CurrentEpisodeId,
                    ShuffleQueue = new Queue<string>(run.ShuffleQueue)
                };
                simulatedRuns[runId] = state;
            }

            var episodes = GetEpisodesInRange(run, show);
            if (!episodes.Any()) return null;

            Episode? episode;

            if (run.Randomize)
            {
                // Always pull from the queue. CurrentEpisodeId tracks the last-played episode
                // (used as the exclusion seed when rebuilding the queue).
                if (state.ShuffleQueue.Count == 0)
                {
                    foreach (var id in BuildShuffleQueue(episodes, state.CurrentEpisodeId))
                        state.ShuffleQueue.Enqueue(id);
                }
                var nextId = state.ShuffleQueue.Dequeue();
                episode = episodes.FirstOrDefault(e => e.Id == nextId) ?? episodes.First();
                state.CurrentEpisodeId = episode.Id;
            }
            else
            {
                episode = episodes.FirstOrDefault(e => e.Id == state.CurrentEpisodeId) ?? episodes.First();
                var nextIndex = (episodes.IndexOf(episode) + 1) % episodes.Count;
                state.CurrentEpisodeId = episodes[nextIndex].Id;
            }

            return episode;
        }

        private List<Episode> GetEpisodesInRange(ShowRun run, Show show)
        {
            var episodes = show.Episodes; // already sorted by Season then Number
            int firstIndex = 0;
            int lastIndex = episodes.Count - 1;

            if (!string.IsNullOrEmpty(run.FirstEpisodeId))
            {
                var idx = episodes.FindIndex(e => e.Id == run.FirstEpisodeId);
                if (idx >= 0) firstIndex = idx;
            }

            if (!string.IsNullOrEmpty(run.LastEpisodeId))
            {
                var idx = episodes.FindIndex(e => e.Id == run.LastEpisodeId);
                if (idx >= 0) lastIndex = idx;
            }

            if (firstIndex > lastIndex)
                return episodes; // Invalid bounds, use full list as fallback

            return episodes.GetRange(firstIndex, lastIndex - firstIndex + 1);
        }

        private List<string> BuildShuffleQueue(List<Episode> episodes, string? excludeId = null)
        {
            var ids = episodes.Select(e => e.Id).ToList();
            // Only exclude if there's more than one episode to avoid an empty queue
            if (ids.Count > 1 && excludeId != null)
                ids = ids.Where(id => id != excludeId).ToList();

            // Fisher-Yates shuffle
            for (int i = ids.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (ids[i], ids[j]) = (ids[j], ids[i]);
            }
            return ids;
        }
    }
}
