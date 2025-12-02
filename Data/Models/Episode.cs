namespace VideoScheduler.Data.Models
{
    public class Episode : Asset
    {
        public string ShowId { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public string Synopsis { get; set; }
    }
}
