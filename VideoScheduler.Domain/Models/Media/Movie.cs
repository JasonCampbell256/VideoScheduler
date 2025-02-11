using VideoScheduler.Domain.Interfaces;

namespace VideoScheduler.Domain.Models.Media
{
    public class Movie : IPlayable
    {
        public required MediaFile MediaFile { get; set; }
        public string Title { get; set; }

        private void populateTitle()
        {
            if (!String.IsNullOrWhiteSpace(MediaFile.MetaData.Title))
            {

            }
        }
    }
}
