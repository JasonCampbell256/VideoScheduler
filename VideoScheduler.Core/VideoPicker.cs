using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class VideoPicker
    {
        public VideoLibrary VideoLibrary { get; set; }

        public VideoPicker(VideoLibrary videoLibrary)
        {
            VideoLibrary = videoLibrary;
        }

        public static TimeSpan GetDuration(string path)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0);
            var file = TagLib.File.Create(path);
            duration = file.Properties.Duration;
            return duration;
        }

        public List<IVideo> GetVideosForTimeBlock(TimeBlock timeBlock)
        {
            var nonCommercialVideos = new List<IVideo>();
            var contentRepository = new ContentRepository(VideoLibrary);
            var commercialFillers = new List<CommercialFiller>();
            var blockDuration = timeBlock.EndTime - timeBlock.StartTime;
            var videoDurations = new TimeSpan(0, 0, 0);
            var random = new Random();

            foreach (var guid in timeBlock.ContentGuids)
            {
                var content = contentRepository.GetContent(guid);

                if (content is AttributeNode)
                {
                    var possibleVideos = GetVideos(content);
                    var randomVideo = possibleVideos[random.Next(0, possibleVideos.Count)];
                    nonCommercialVideos.Add(randomVideo);
                }
                else if (content is BlockTemplateItem)
                {
                    nonCommercialVideos.AddRange(GetVideos(content));
                }
                if (content is ShowRun)
                {
                    var showRun = (ShowRun)content;
                    nonCommercialVideos.Add(showRun.NextEpisode);
                    showRun.NextEpisode = showRun.NextEpisode.GetNextEpisode();
                    showRun.NextEpisodeFilePath = showRun.NextEpisode.FilePath;
                    contentRepository.ShowRunManager.AddOrUpdateShowRun(showRun);
                }
                else if (content is CommercialFiller)
                {
                    commercialFillers.Add((CommercialFiller)content);
                    nonCommercialVideos.Add(null);
                }
                
            }

            foreach (var video in nonCommercialVideos)
            {
                if (video != null)
                {
                    videoDurations += GetDuration(video.FilePath);
                }
            }

            var timeSpanPerCommercialBreak = new TimeSpan(0, 0, 0);
            if (commercialFillers.Count > 0)
            {
                timeSpanPerCommercialBreak = new TimeSpan((blockDuration.Ticks - videoDurations.Ticks) / commercialFillers.Count);
            }
            List<IVideo> videos = new List<IVideo>();
            int commercialBreakCount = 0;
            foreach (var video in nonCommercialVideos)
            {
                if (video != null)
                {
                    videos.Add(video);
                } else
                {
                    if (commercialFillers.Count >= commercialBreakCount + 1)
                    {
                        var commercialFiller = commercialFillers[commercialBreakCount];
                        var leftoverTimeSpan = timeSpanPerCommercialBreak;
                        var possibleVideos = new List<IVideo>();
                        var videosInBreak = new List<IVideo>();
                        foreach (var guid in commercialFiller.Attributes)
                        {
                            var attributeTree = contentRepository.GetContent(guid);
                            possibleVideos.AddRange(GetVideos(attributeTree));
                        }
                        if (possibleVideos.Count > 0)
                        {
                            var ticksPerSecond = 10000000;
                            while (leftoverTimeSpan.Ticks > 20 * ticksPerSecond)
                            {
                                var randomVideo = possibleVideos[random.Next(0, possibleVideos.Count)];
                                possibleVideos.Remove(randomVideo);
                                videosInBreak.Add(randomVideo);
                                videoDurations += GetDuration(randomVideo.FilePath);
                                leftoverTimeSpan = leftoverTimeSpan - GetDuration(randomVideo.FilePath);
                            }
                        }
                        videos.AddRange(videosInBreak);
                    }
                }
            }


            return videos;
        }

        public ISchedulableContent GetContent(Guid contentGuid)
        {
            var contentRepository = new ContentRepository(VideoLibrary);
            return contentRepository.GetContent(contentGuid);
        }


        public List<IVideo> GetVideos(ISchedulableContent schedulableContent)
        {
            var videos = new List<IVideo>();
            if (schedulableContent is AttributeNode)
            {
                var tree = (AttributeNode)schedulableContent;
                var foundVideos = VideoLibrary.Video.Where(video => LibraryManager.MatchAttributeTree(video.AttributeTree, tree)).ToList();
                videos.AddRange(foundVideos);
            }
            else if (schedulableContent is BlockTemplateItem)
            {
                var contentRepository = new ContentRepository(VideoLibrary);
                var blockTemplateItem = (BlockTemplateItem)schedulableContent;
                foreach (var bumperInfoTreeGuid in blockTemplateItem.BeforeBumpersAttributeTrees)
                {
                    var bumperInfo = (AttributeNode)contentRepository.GetContent(bumperInfoTreeGuid);
                    videos.AddRange(GetVideos(bumperInfo));
                }

                var showRun = (ShowRun)contentRepository.GetContent(blockTemplateItem.ShowRunGuid);
                videos.Add(showRun.NextEpisode);
                showRun.NextEpisode = showRun.NextEpisode.GetNextEpisode();
                contentRepository.ShowRunManager.AddOrUpdateShowRun(showRun);

                foreach (var bumperInfoTreeGuid in blockTemplateItem.AfterBumpersAttributeTrees)
                {
                    var bumperInfo = (AttributeNode)contentRepository.GetContent(bumperInfoTreeGuid);
                    videos.AddRange(GetVideos(bumperInfo));
                }
            }
            else if (schedulableContent is SchedulableBlock)
            {
                var schedulableBlock = (SchedulableBlock)schedulableContent;
                foreach (var contentGuid in schedulableBlock.ContentGuids)
                {
                    var contentRepository = new ContentRepository(VideoLibrary);
                    var content = contentRepository.GetContent(contentGuid);

                    if (content is AttributeNode)
                    {
                        videos.Add(GetVideos((AttributeNode)content).First());
                    }
                    else if (content is BlockTemplateItem)
                    {
                        videos.AddRange(GetVideos((BlockTemplateItem)content));
                    }
                }
            }
            else if (schedulableContent is ShowRun)
            {

            }
            return videos;
            
        }

       

        public Video GetVideoByPath(string path)
        {
            if (VideoLibrary.Video.Where(video => video.FilePath == path).ToList().Count > 0)
            {
                return VideoLibrary.Video.Where(video => video.FilePath == path).First();
            }
            return null;
        }
    }
}
