using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class BlockTemplateItem : ISchedulableContent
    {
        [JsonProperty]
        public Guid Guid { get; set; } = Guid.NewGuid();
        [JsonProperty]
        public List<Guid> BeforeBumpersAttributeTrees = new List<Guid>();
        [JsonProperty]
        public List<Guid> AfterBumpersAttributeTrees = new List<Guid>();
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public Guid ShowRunGuid { get; set; }

        public BlockTemplateItem()
        {
        }

        public BlockTemplateItem (ShowRun showRun)
        {
            ShowRunGuid = showRun.Guid;
        }

        public BlockTemplateItem (Guid guid)
        {
            ShowRunGuid = guid;
        }

    }
}
