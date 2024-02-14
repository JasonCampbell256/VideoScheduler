using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoScheduler.Domain
{
    //TODO this class isn't being used yet, but the idea is to define a block that can be referenced for re-use
    public class SchedulableBlock : ISchedulableContent
    {
        [JsonProperty]
        public Guid Guid { get; set; }
        [JsonProperty]
        public List<Guid> ContentGuids = new List<Guid>();
        [JsonProperty]
        public string Description { get; set; }

        public SchedulableBlock()
        {
            Guid = Guid.NewGuid();
        }

        public void AddContent(ISchedulableContent content)
        {
            ContentGuids.Add(content.Guid);
        }

        public void AddContent(Guid contentGuid)
        {
            ContentGuids.Add(contentGuid);
        }
    }
}
