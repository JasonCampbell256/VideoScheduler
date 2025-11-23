using System;
using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class DailySchedule
    {
        public DateTime Date { get; set; }
        public List<ScheduledSlot> Slots { get; set; } = new List<ScheduledSlot>();
    }
}
