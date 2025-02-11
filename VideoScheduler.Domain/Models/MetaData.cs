namespace VideoScheduler.Domain.Models
{
    public class MetaData
    {
        public string Title { get; set; } = string.Empty;
        public TimeSpan Duration {  get; set; } = TimeSpan.FromSeconds(0);
    }
}
