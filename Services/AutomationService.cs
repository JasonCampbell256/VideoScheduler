using VideoScheduler.Data;
using VideoScheduler.Data.Models;
using Timer = System.Threading.Timer;

namespace VideoScheduler.Services
{
    public class AutomationService : IHostedService, IDisposable
    {
        private readonly SchedulePlanner _planner;
        private readonly VlcService _vlc;
        private readonly YamlRepository _yamlRepository;
        private Timer? _timer;
        private bool _isSyncing = false;
        private HashSet<string> _playedItems = new HashSet<string>();

        // Max seconds to let a finishing video overrun before hard-cutting,
        // and min seconds into the next video before seeking (to absorb load time).
        private const int TransitionGraceSec = 5;

        public bool IsAutoPlaying { get; set; }
        public VlcStatus? LastStatus { get; private set; }
        public event Action? OnStateChanged;
        public event Action? OnStatusUpdated;

        public AutomationService(SchedulePlanner planner, VlcService vlc, YamlRepository yamlRepository)
        {
            _planner = planner;
            _vlc = vlc;
            _yamlRepository = yamlRepository;
            IsAutoPlaying = _yamlRepository.AppState.IsAutoPlaying;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _planner.Initialize();
            _timer = new Timer(async _ => await SyncLoop(), null, 0, 250);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void ToggleAutoPlay(bool enable)
        {
            IsAutoPlaying = enable;
            _yamlRepository.AppState.IsAutoPlaying = enable;
            _yamlRepository.SaveAppState();
            OnStateChanged?.Invoke();
        }

        private bool IsCurrentItem(PlaylistItem item, List<PlaylistItem> playlist)
        {
            var now = DateTime.Now;
            var index = playlist.IndexOf(item);
            
            if (index == -1) return false;

            var naturalEndTime = item.EstimatedStartTime.AddSeconds(item.DurationSec);
            DateTime endTime = naturalEndTime;
            
            if (index < playlist.Count - 1)
            {
                var nextItem = playlist[index + 1];
                // If the next item starts before this one naturally ends, cut it short.
                // Otherwise, leave it alone (do not extend to fill gaps).
                if (nextItem.EstimatedStartTime < naturalEndTime)
                {
                    endTime = nextItem.EstimatedStartTime;
                }
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

                var expectedItem = currentPlaylist.FirstOrDefault(i => IsCurrentItem(i, currentPlaylist));

                if (expectedItem == null) return;

                // If we failed to get status earlier, we can't sync
                if (!_vlc.IsConnected) return;

                var expectedFilename = Path.GetFileName(expectedItem.FilePath);
                var currentVlcFilename = status.Filename;
                
                bool isWrongFile = string.IsNullOrEmpty(currentVlcFilename) || 
                                   !string.IsNullOrEmpty(expectedFilename) && !currentVlcFilename.Contains(expectedFilename, StringComparison.OrdinalIgnoreCase);

                // Grace period: if VLC is playing the previous video and it's almost
                // done, let it finish rather than hard-cutting (which would also cause
                // the next video to seek past its first few seconds).
                if (isWrongFile && status.State == VlcState.Playing && status.Length > 0)
                {
                    var remainingTime = status.Length - status.Time;
                    if (remainingTime > 0 && remainingTime <= TransitionGraceSec)
                    {
                        return;
                    }
                }

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

                var expectedOffset = (DateTime.Now - expectedItem.EstimatedStartTime).TotalSeconds;

                if (status.State == VlcState.Stopped || status.State == VlcState.Unknown || isWrongFile)
                {
                    Console.WriteLine($"Automation: Playing {expectedItem.Title}");
                    await _vlc.Play(expectedItem.FilePath);
                    await Task.Delay(50);
                    // Only seek if we're well into the video's timeline. Small offsets
                    // are just loading delay — seeking past them skips content.
                    if (expectedOffset > TransitionGraceSec)
                    {
                        await _vlc.Seek((int)expectedOffset);
                    }
                }
                else if (status.State == VlcState.Playing || status.State == VlcState.Paused)
                {
                    var actualOffset = status.Time;
                    var drift = Math.Abs(expectedOffset - actualOffset);
                    
                    if (drift > 10)
                    {
                        Console.WriteLine($"Automation: Correcting drift ({drift}s)");
                        await _vlc.Seek((int)expectedOffset);
                        if (status.State == VlcState.Paused)
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
