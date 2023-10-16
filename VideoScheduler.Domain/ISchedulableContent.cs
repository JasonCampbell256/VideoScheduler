using System;
using System.Collections.Generic;
using System.Text;

namespace VideoScheduler.Domain
{
    public interface ISchedulableContent
    {
        Guid Guid { get; }
        string Description { get; }
    }
}
