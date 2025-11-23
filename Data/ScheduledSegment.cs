using System;

namespace VideoScheduler.Data
{
    public class ScheduledSegment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TemplateId { get; set; }
    }
}
