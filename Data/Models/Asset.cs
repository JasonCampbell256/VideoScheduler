using System.Collections.Generic;

namespace VideoScheduler.Data.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public int DurationSec { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string FilePath { get; set; }
    }
}
