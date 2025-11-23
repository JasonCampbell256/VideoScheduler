namespace VideoScheduler.Data
{
    public class InjectionRule
    {
        public BlockItemType TargetBlockType { get; set; } 
        public ContentType? TargetCategory { get; set; }
        public string AssetId { get; set; }
        public double Probability { get; set; }
    }
}
