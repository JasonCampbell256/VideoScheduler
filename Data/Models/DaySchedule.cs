using System;
using System.Collections.Generic;

namespace VideoScheduler.Data.Models
{
    public class DaySchedule
    {
        public DayOfWeek Day { get; set; }
        public List<ScheduleTimeBlock> TimeBlocks { get; set; } = new List<ScheduleTimeBlock>();
    }
}
