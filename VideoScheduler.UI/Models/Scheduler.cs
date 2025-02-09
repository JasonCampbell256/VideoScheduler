using System;
using System.Collections.Generic;
using System.IO;
using VideoScheduler.Core;
using VideoScheduler.Domain;

namespace VideoScheduler.UI.Models
{
    public class Scheduler
    {
        private readonly PersistenceManagers _persistenceManagers;
        private List<TimeBlock> _timeBlocks = new List<TimeBlock>();
        private TimeBlock currentTimeBlock = null;

        public Scheduler()
        {
            var libraryPath = PersistenceManagers.GetFilePath();
            _persistenceManagers = new PersistenceManagers();
        }

        public void LoadSchedule()
        {
            _timeBlocks = _persistenceManagers.timeBlockManager.GetTimeBlocks(System.DateTime.Now.DayOfWeek);
        }

        public Queue<string> GetVideosForCurrentTimeBlock()
        {
            var playlist = new Queue<string>();

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            var truncatedCurrentTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

            foreach (var timeBlock in _timeBlocks)
            {
                if (truncatedCurrentTime >= timeBlock.StartTime && truncatedCurrentTime < timeBlock.EndTime)
                {
                    if (currentTimeBlock != null && currentTimeBlock.Guid == timeBlock.Guid)
                    {
                        return playlist;
                    }

                    currentTimeBlock = timeBlock;
                    var videos = _persistenceManagers._picker.GetVideosForTimeBlock(timeBlock);
                    var timeElapsed = truncatedCurrentTime - currentTimeBlock.StartTime;

                    var timespanCounter = new TimeSpan(0, 0, 0);
                    var timespanToStart = new TimeSpan(0, 0, 0);
                    var isFirstVideoFound = false;

                    foreach (var video in videos)
                    {
                        var videoDuration = VideoPicker.GetDuration(video.FilePath);
                        timespanCounter += videoDuration;
                        if (timespanCounter >= timeElapsed)
                        {
                            playlist.Enqueue(video.FilePath);
                            if (!isFirstVideoFound)
                            {
                                isFirstVideoFound = true;
                                timespanToStart = timeElapsed - (timespanCounter - videoDuration);
                            }
                        }
                    }
                    break;
                }
            }

            return playlist;
        }
    }
}
