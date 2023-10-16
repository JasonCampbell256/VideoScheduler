using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoScheduler.Domain
{
    public class Schedule
    {
        private List<TimeBlock> timeBlocks = new List<TimeBlock>();

        public void AddTimeBlock(TimeBlock block)
        {
            timeBlocks.Add(block);
        }

        public bool RemoveTimeBlock(TimeBlock block)
        {
            return timeBlocks.Remove(block);
        }

        public IEnumerable<TimeBlock> Query(DayOfWeek day, TimeSpan time)
        {
            return timeBlocks.Where(block => block.Day == day && block.IsTimeInRange(time));
        }

        public IEnumerable<TimeBlock> Query(DayOfWeek day)
        {
            return timeBlocks.Where(block => block.Day == day);
        }
    }
}
