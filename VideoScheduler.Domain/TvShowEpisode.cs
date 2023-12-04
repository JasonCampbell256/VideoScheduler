using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class TvShowEpisode : IVideo
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public TvShowSeason Season { get; set; }
        public int EpisodeNumber { get; set; }

        public TvShowEpisode(TvShowSeason season, int episodeNumber, string filePath)
        {
            Season = season;
            EpisodeNumber = episodeNumber;
            FilePath = filePath;
            FileName = System.IO.Path.GetFileName(filePath);
        }

        public TvShowEpisode GetNextEpisode()
        {
            var seasonNumber = Season.SeasonNumber;
            var show = Season.Show;

            //Check if there is a next episode in this season
            if (EpisodeNumber < Season.Episodes.Last().EpisodeNumber)
            {
                for (int i = 0; i < Season.Episodes.Count; i++)
                {
                    if (Season.Episodes[i].EpisodeNumber > EpisodeNumber)
                    {
                        return Season.Episodes[i];
                    }
                }
            }
            else if (seasonNumber < show.Seasons.Last().SeasonNumber)
            {
                for (int i = 0; i < show.Seasons.Count; i++)
                {
                    if (show.Seasons[i].SeasonNumber > seasonNumber)
                    {
                        return show.Seasons[i].Episodes[0];
                    }
                }
            }

            //Return the first episode of the first season
            return show.Seasons[0].Episodes[0];
        }

        public override string ToString()
        {
            return Season.Show.Title + " S" + Season.SeasonNumber + " E" + EpisodeNumber;
        }
    }
}
