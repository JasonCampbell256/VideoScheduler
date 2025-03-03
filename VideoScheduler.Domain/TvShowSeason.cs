using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class TvShowSeason : IComparable<TvShowSeason>
    {
        public TvShow Show { get; set; }
        public int SeasonNumber { get; set; }
        public List<TvShowEpisode> Episodes { get; set; }

        public TvShowSeason(TvShow show, int seasonNumber)
        {
            Show = show;
            SeasonNumber = seasonNumber;
            Episodes = new List<TvShowEpisode>();
        }

        public override string ToString()
        {
            return SeasonNumber.ToString();
        }

        public int CompareTo(TvShowSeason other)
        {
            return this.SeasonNumber.CompareTo(other.SeasonNumber);
        }
    }
}
