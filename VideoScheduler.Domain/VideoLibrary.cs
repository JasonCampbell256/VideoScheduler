using System.Collections.Generic;
using System.Linq;

namespace VideoScheduler.Domain
{
    public class VideoLibrary
    {
        public List<Video> Video = new List<Video>();
        public List<TvShow> Shows = new List<TvShow>();
        public List<Movie> Movies = new List<Movie>();

        public TvShowEpisode GetShowEpisode(string showName, int seasonNumber, int episodeNumber)
        {
            return Shows.Where(show => show.Title == showName).First().Seasons.Where(season => season.SeasonNumber == seasonNumber).First().Episodes.Where(episode => episode.EpisodeNumber == episodeNumber).First();
        }

        public TvShowEpisode GetShowEpisode(string filePath)
        {
            return Shows.SelectMany(show => show.Seasons).SelectMany(season => season.Episodes).Where(episode => episode.FilePath == filePath).First();
        }

        public TvShow GetShow(string showName)
        {
            return Shows.FirstOrDefault(show => show.Title == showName);
        }

        public Movie GetMovie(SchedulableMovie schedulableMovie)
        {
            return Movies.Where(movie => movie.FilePath == schedulableMovie.MovieFilePath).FirstOrDefault();
        }
    }
}
