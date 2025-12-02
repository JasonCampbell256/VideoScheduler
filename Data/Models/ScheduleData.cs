using System.Collections.Generic;

namespace VideoScheduler.Data.Models
{
    public class ScheduleData
    {
        public List<DaySchedule> Days { get; set; } = new List<DaySchedule>();
    }
}
