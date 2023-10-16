using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public interface IVideo
    {
        string FileName { get; set; }
        string FilePath { get; set; }
    }
}
