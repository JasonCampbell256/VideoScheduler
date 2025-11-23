using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class AssetGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GroupType Type { get; set; } = GroupType.General;
        public List<string> AssetIds { get; set; } = new List<string>();
    }
}
