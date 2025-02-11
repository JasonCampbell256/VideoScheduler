using VideoScheduler.Domain.Interfaces;

namespace VideoScheduler.Domain.Models.Media
{
    public class StandaloneMedia : IPlayable
    {
        public required MediaFile MediaFile { get; set; }
    }
}
