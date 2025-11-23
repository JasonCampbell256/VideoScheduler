using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class Show
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int DefaultDurationSec { get; set; }
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
