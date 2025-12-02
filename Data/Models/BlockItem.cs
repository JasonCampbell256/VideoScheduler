using System.Collections.Generic;

namespace VideoScheduler.Data.Models
{
    public class BlockItem
    {
        public BlockItemType Type { get; set; }
        
        // For Fixed
        public string AssetId { get; set; }

        // For Dynamic
        public ContentType? Category { get; set; }
        public string GroupId { get; set; } // Target a specific AssetGroup
        public int? Duration { get; set; }
        public List<string> Tags { get; set; }

        // For ContentSegment
        public int? SegmentIndex { get; set; }

        // For Show Run
        public string RunId { get; set; }

        // For Commercial Break
        public bool AutoDuration { get; set; }
    }
}
