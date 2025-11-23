namespace VideoScheduler.Data
{
    public class ScheduledSlot
    {
        public string Time { get; set; }
        public string TemplateId { get; set; }
        public ScheduledContent Content { get; set; }
    }
}
