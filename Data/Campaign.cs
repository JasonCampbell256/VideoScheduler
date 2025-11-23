using System;
using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class Campaign
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<InjectionRule> Injections { get; set; } = new List<InjectionRule>();
    }
}
