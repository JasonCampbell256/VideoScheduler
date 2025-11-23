using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class LibraryData
    {
        public List<Show> Shows { get; set; } = new List<Show>();
        public List<Asset> Movies { get; set; } = new List<Asset>();
        public List<Asset> Bumpers { get; set; } = new List<Asset>();
        public List<Asset> Commercials { get; set; } = new List<Asset>();
        public List<AssetGroup> Groups { get; set; } = new List<AssetGroup>();
        public List<ShowRun> Runs { get; set; } = new List<ShowRun>();
        public string? LastScanPath { get; set; }
    }
}
