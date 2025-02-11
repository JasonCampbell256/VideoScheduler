namespace VideoScheduler.Domain.Models
{
    public class MediaFile
    {
        public String FilePath { get; set; }
        public MetaData MetaData { get; set; } = new ();
        
        public MediaFile(string filePath) 
        {
            FilePath = filePath;
        }
    }
}
