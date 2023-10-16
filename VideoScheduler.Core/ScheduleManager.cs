using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class ScheduleManager
    {
        Dictionary<DayOfWeek, List<TimeBlock>> weeklyBlocks = new Dictionary<DayOfWeek, List<TimeBlock>>();
        List<TimeBlock> customDateBlocks = new List<TimeBlock>();

        /*public ScheduleManager()
        {
            Schedule = new List<SchedulableBlock>();
        }

        public void AddScheduledBlock(SchedulableBlock block)
        {
            Schedule.Add(block);
        }*/

        //public List<SchedulableBlock> GetScheduledBlocks(DayOfWeek day)
        //{
        //    return Schedule.Where(s => s.TimeInfo.Day == day).ToList();
        //}

        //public List<SchedulableBlock> GetScheduledBlocks(DateTime date)
        //{
        //    return Schedule.Where(s => s.TimeInfo.Date == date).ToList();
        //}
    }
}
