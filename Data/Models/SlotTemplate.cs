using System.Collections.Generic;

namespace VideoScheduler.Data.Models
{
    public class SlotTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalDurationSec { get; set; }
        public List<BlockItem> Structure { get; set; } = new List<BlockItem>();
    }
}
