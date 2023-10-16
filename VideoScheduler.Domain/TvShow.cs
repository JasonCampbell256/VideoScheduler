using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class TvShow
    {
        public string Title { get; set; }
        public List<TvShowSeason> Seasons = new List<TvShowSeason>();
    }
}
