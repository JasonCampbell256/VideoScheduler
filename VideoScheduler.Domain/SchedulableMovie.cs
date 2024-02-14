using Newtonsoft.Json;
using System;

namespace VideoScheduler.Domain
{
    public class SchedulableMovie : ISchedulableContent
    {
        [JsonProperty]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public string MovieFilePath { get; set; }

        public string Description { get; set; }

        public SchedulableMovie()
        {
        }

        public SchedulableMovie(Movie movie)
        {
            MovieFilePath = movie.FilePath;
        }

    }
}
