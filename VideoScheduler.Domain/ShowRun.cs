using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class ShowRun : ISchedulableContent
    {
        [JsonProperty]
        public Guid Guid { get; set; } = Guid.NewGuid();
        [JsonProperty]
        public string RunName { get; set; }

        [JsonIgnore]
        public TvShowEpisode NextEpisode { get; set; }

        [JsonProperty]
        public string NextEpisodeFilePath { get; set; }

        public string Description { get; set; }

        public ShowRun (string runName, TvShowEpisode nextEpisode)
        {
            RunName = runName;
            NextEpisode = nextEpisode;
            if (NextEpisode != null) { 
                NextEpisodeFilePath = NextEpisode.FilePath;
            }
        }

        public string GetShowTitle()
        {
            return NextEpisode.Season.Show.Title;
        }
    }
}
