using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class Movie : IVideo
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
