namespace VideoScheduler.Data.Models
{
    public class ShowRun
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShowId { get; set; }
        public string CurrentEpisodeId { get; set; }
        public string? FirstEpisodeId { get; set; }
        public string? LastEpisodeId { get; set; }
        public bool Randomize { get; set; }
        public List<string> ShuffleQueue { get; set; } = new List<string>();
    }
}
