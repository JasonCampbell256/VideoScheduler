using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoScheduler.Domain
{
    public class CommercialFiller : ISchedulableContent
    {
        [JsonProperty]
        public Guid Guid { get; set; }
        [JsonProperty]
        public List<Guid> Attributes { get; set; }
        [JsonProperty]
        public string Description { get; set; }

        public CommercialFiller()
        {
            Guid = Guid.NewGuid();
            Attributes = new List<Guid>();
        }
    }
}
