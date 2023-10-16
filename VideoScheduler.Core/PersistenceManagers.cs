using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class PersistenceManagers
    {
        public VideoLibrary _library = LibraryManager.LoadLibrary(GetFilePath());
        public VideoPicker _picker;
        public ScheduleManager scheduleManager = new ScheduleManager();
        public SchedulableBlockManager schedulableBlockManager = new SchedulableBlockManager();
        public AttributeTreeManager attributeTreeManager = new AttributeTreeManager();
        public BlockTemplateItemManager blockTemplateItemManager = new BlockTemplateItemManager();
        public TimeBlockManager timeBlockManager = new TimeBlockManager();
        public CommercialFillerManager commercialFillerManager = new CommercialFillerManager();
        public ShowRunManager runManager;

        public PersistenceManagers()
        {
            _picker = new VideoPicker(_library);
            runManager = new ShowRunManager(_library);
        }

        public static string GetFilePath()
        {
            return JObject.Parse(File.ReadAllText(@"settings.json"))["FilePath"].ToString();
        }
    }
}
