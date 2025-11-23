using System.Collections.Generic;

namespace VideoScheduler.Data
{
    public class ScheduleData
    {
        public List<DaySchedule> Days { get; set; } = new List<DaySchedule>();
    }
}
