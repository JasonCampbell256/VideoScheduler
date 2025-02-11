using VideoScheduler.Domain.Models;

namespace VideoScheduler.Domain.Interfaces
{
    public interface IPlayable
    {
        MediaFile MediaFile { get; }
    }
}
