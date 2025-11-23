using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace VideoScheduler.Data
{
    public class ScheduleTimeBlock
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        
        [YamlIgnore]
        public TimeOnly StartTime { get; set; }
        
        [YamlIgnore]
        public TimeOnly EndTime { get; set; }

        [YamlMember(Alias = "startTime")]
        public string StartTimeStr 
        { 
            get => StartTime.ToString("HH:mm");
            set => StartTime = TimeOnly.Parse(value);
        }

        [YamlMember(Alias = "endTime")]
        public string EndTimeStr 
        { 
            get => EndTime.ToString("HH:mm");
            set => EndTime = TimeOnly.Parse(value);
        }

        public List<ScheduledSegment> Segments { get; set; } = new List<ScheduledSegment>();
    }
}
