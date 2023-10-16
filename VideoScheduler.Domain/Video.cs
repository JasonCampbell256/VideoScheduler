using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class Video : IVideo, IFilterable
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public AttributeNode AttributeTree { get; set; }

        public Video(string fileName, string filePath, AttributeNode attributeTree)
        {
            FileName = fileName;
            FilePath = filePath;
            AttributeTree = attributeTree;
        }
    }
}
