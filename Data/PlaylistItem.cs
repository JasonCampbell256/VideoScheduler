using System;

namespace VideoScheduler.Data
{
    public class PlaylistItem
    {
        public DateTime EstimatedStartTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public double DurationSec { get; set; }
        public string Type { get; set; } = string.Empty;
        public string AssetId { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string? RunId { get; set; }
        public string? EpisodeId { get; set; }
    }
}
