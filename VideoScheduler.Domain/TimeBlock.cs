﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VideoScheduler.Domain
{
    public class TimeBlock
    {
        [JsonProperty]
        public Guid Guid { get; set; } = Guid.NewGuid();
        [JsonProperty]
        public DayOfWeek? Day { get; set; }
        [JsonProperty]
        public TimeSpan StartTime { get; set; }
        [JsonProperty]
        public TimeSpan EndTime { get; set; }
        [JsonProperty]
        public DateTime? Date { get; set; }
        [JsonProperty]
        public List<Guid> ContentGuids { get; set; } = new List<Guid>();
        [JsonProperty]
        public string Description { get; set; }


        public TimeBlock (DayOfWeek day, TimeSpan startTime, TimeSpan endTime, DateTime? date = null, string description = null)
        {
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
            Date = date;
            Description = description;
        }

        public bool IsTimeInRange(TimeSpan time)
        {
            return time >= StartTime && time <= EndTime;
        }
    }
}
