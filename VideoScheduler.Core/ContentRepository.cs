using System;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class ContentRepository
    {
        public readonly ShowRunManager ShowRunManager;
        public readonly MovieRunManager MovieRunManager = new MovieRunManager();
        public readonly AttributeTreeManager AttributeTreeManager = new AttributeTreeManager();
        public readonly SchedulableBlockManager SchedulableBlockManager = new SchedulableBlockManager();
        public readonly BlockTemplateItemManager BlockTemplateItemManager = new BlockTemplateItemManager();
        public readonly CommercialFillerManager CommercialFillerManager = new CommercialFillerManager();

        public ContentRepository(VideoLibrary videoLibrary)
        {
            ShowRunManager = new ShowRunManager(videoLibrary);
        }

        public ISchedulableContent GetContent(Guid GUID)
        {
            if (ShowRunManager.GetShowRun(GUID) != null)
            {
                return ShowRunManager.GetShowRun(GUID);
            }
            if (AttributeTreeManager.GetAttributeTree(GUID) != null)
            {
                return AttributeTreeManager.GetAttributeTree(GUID);
            }
            if (SchedulableBlockManager.GetSchedulableBlock(GUID) != null)
            {
                return SchedulableBlockManager.GetSchedulableBlock(GUID);
            }
            if (BlockTemplateItemManager.GetBlockTemplateItem(GUID) != null)
            {
                return BlockTemplateItemManager.GetBlockTemplateItem(GUID);
            }
            if (CommercialFillerManager.GetCommercial(GUID) != null)
            {
                return CommercialFillerManager.GetCommercial(GUID);
            }
            if (MovieRunManager.GetMovieRun(GUID) != null)
            {
                return MovieRunManager.GetMovieRun(GUID);
            }
            return null;
        }
    }
}
