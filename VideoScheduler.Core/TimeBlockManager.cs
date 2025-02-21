using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class TimeBlockManager
    {
        private Dictionary<Guid, TimeBlock> _timeBlocks = new Dictionary<Guid, TimeBlock>();

        public void AddOrUpdateTimeBlock(TimeBlock timeBlock)
        {
            // Load existing time blocks, if not already done
            LoadTimeBlocks();

            // Filter existing blocks by the same DayOfWeek
            var existingBlocksForDay = _timeBlocks.Values.Where(tb => tb.Day == timeBlock.Day).ToList();

            // No overlaps found, add or update the time block
            _timeBlocks[timeBlock.Guid] = timeBlock;

            // Save updated time blocks
            SaveTimeBlocks();
        }

        public void RemoveTimeBlock(TimeBlock timeBlock)
        {
            _timeBlocks.Remove(timeBlock.Guid);
            SaveTimeBlocks();
        }

        public void RemoveAllTimeBlocksInDay(DayOfWeek day)
        {
            GetTimeBlocks(day).ForEach(RemoveTimeBlock);
        }

        public void CopyTimeBlockToDay(TimeBlock timeBlock, DayOfWeek day)
        {
            var newBlock = timeBlock;
            timeBlock.Guid = Guid.NewGuid();
            timeBlock.Day = day;
            AddOrUpdateTimeBlock(timeBlock);
        }

        public List<TimeBlock> GetTimeBlocks(DayOfWeek day) 
        {
            // Load existing time blocks, if not already done
            LoadTimeBlocks();

            // Filter existing blocks by the same DayOfWeek
            var existingBlocksForDay = _timeBlocks.Values.Where(tb => tb.Day == day).ToList();

            // Order the blocks by start time
            existingBlocksForDay = existingBlocksForDay.OrderBy(tb => tb.StartTime).ToList();

            return existingBlocksForDay;
        }

        private void LoadTimeBlocks()
        {
            if (File.Exists("timeBlocks.json"))
            {
                string json = File.ReadAllText("timeBlocks.json");
                _timeBlocks = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, TimeBlock>>(json);
            }
            if (_timeBlocks == null)
            {
                _timeBlocks = new Dictionary<Guid, TimeBlock>();
            }
        }

        public void SaveTimeBlocks()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_timeBlocks);

            string tempFile = "temp_timeBlocks.json";

            string actualFile = "timeBlocks.json";

            try
            {
                File.WriteAllText(tempFile, json);
                if (!File.Exists(actualFile))
                {
                    File.Create(actualFile);
                }
                File.Replace(tempFile, actualFile, null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
