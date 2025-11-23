using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VideoScheduler.Data
{
    public class AutomationService : IDisposable
    {
        private readonly SchedulePlanner _planner;
        private readonly VlcService _vlc;
        private Timer? _timer;
        private bool _isSyncing = false;
        private HashSet<string> _playedItems = new HashSet<string>();

        public bool IsAutoPlaying { get; set; }
        public VlcStatus? LastStatus { get; private set; }
        public event Action? OnStateChanged;
        public event Action? OnStatusUpdated;

        public AutomationService(SchedulePlanner planner, VlcService vlc)
        {
            _planner = planner;
            _vlc = vlc;
            _timer = new Timer(async _ => await SyncLoop(), null, 0, 1000);
        }

        public void ToggleAutoPlay(bool enable)
        {
            IsAutoPlaying = enable;
            OnStateChanged?.Invoke();
        }

        private bool IsCurrentItem(PlaylistItem item, List<PlaylistItem> playlist)
        {
            var now = DateTime.Now;
            var index = playlist.IndexOf(item);
            
            if (index == -1) return false;

            DateTime endTime;
            
            if (index < playlist.Count - 1)
            {
                var nextItem = playlist[index + 1];
                endTime = nextItem.EstimatedStartTime;
            }
            else
            {
                endTime = item.EstimatedStartTime.AddSeconds(item.DurationSec);
            }

            return now >= item.EstimatedStartTime && now < endTime;
        }

        private async Task SyncLoop()
        {
            if (_isSyncing) return;
            _isSyncing = true;

            try
            {
                // 1. Always fetch status for UI
                var status = await _vlc.GetStatusAsync();
                if (!_vlc.IsConnected) 
                {
                    await _vlc.ConnectAsync();
                    if (_vlc.IsConnected) status = await _vlc.GetStatusAsync();
                }

                if (_vlc.IsConnected)
                {
                    LastStatus = status;
                    OnStatusUpdated?.Invoke();
                }

                // 2. Automation Logic
                var currentPlaylist = _planner.CurrentPlaylist;
                if (!IsAutoPlaying || currentPlaylist == null || !currentPlaylist.Any()) return;

                var now = DateTime.Now;
                var expectedItem = currentPlaylist.FirstOrDefault(i => IsCurrentItem(i, currentPlaylist));

                if (expectedItem == null) return;

                // Mark as played and advance run if needed
                var itemKey = $"{expectedItem.AssetId}-{expectedItem.EstimatedStartTime.Ticks}";
                if (!_playedItems.Contains(itemKey))
                {
                    _playedItems.Add(itemKey);
                    if (!string.IsNullOrEmpty(expectedItem.RunId))
                    {
                        _planner.AdvanceRun(expectedItem.RunId);
                    }
                }

                // If we failed to get status earlier, we can't sync
                if (!_vlc.IsConnected) return;

                var expectedFilename = System.IO.Path.GetFileName(expectedItem.FilePath);
                var currentVlcFilename = status.Filename;
                
                bool isWrongFile = string.IsNullOrEmpty(currentVlcFilename) || 
                                   (!string.IsNullOrEmpty(expectedFilename) && !currentVlcFilename.Contains(expectedFilename, StringComparison.OrdinalIgnoreCase));

                var expectedOffset = (now - expectedItem.EstimatedStartTime).TotalSeconds;

                if (status.State == "stopped" || status.State == "ended" || isWrongFile)
                {
                    Console.WriteLine($"Automation: Playing {expectedItem.Title}");
                    await _vlc.Play(expectedItem.FilePath);
                    await Task.Delay(500);
                    if (expectedOffset > 0)
                    {
                        await _vlc.Seek((int)expectedOffset);
                    }
                }
                else if (status.State == "playing" || status.State == "paused")
                {
                    var actualOffset = status.Time;
                    var drift = Math.Abs(expectedOffset - actualOffset);
                    
                    if (drift > 10)
                    {
                        Console.WriteLine($"Automation: Correcting drift ({drift}s)");
                        await _vlc.Seek((int)expectedOffset);
                        if (status.State == "paused")
                        {
                            await _vlc.Play(expectedItem.FilePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sync Error: {ex.Message}");
            }
            finally
            {
                _isSyncing = false;
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
