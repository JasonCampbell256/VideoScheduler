using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class AppStateData
    {
        public List<PlaylistItem> CurrentPlaylist { get; set; } = new List<PlaylistItem>();
        public int ForecastDays { get; set; } = 3;
    }
}
