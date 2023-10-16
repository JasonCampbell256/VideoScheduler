using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class BlockTemplateItemManager
    {
        private Dictionary<Guid, BlockTemplateItem> _blockTemplateItems = new Dictionary<Guid, BlockTemplateItem>();
        private string path = "blockTemplateItems.json";

        public void AddOrUpdateBlockTemplateItem(BlockTemplateItem blockTemplateItem)
        {
            LoadBlockTemplateItems();
            if (_blockTemplateItems.ContainsKey(blockTemplateItem.Guid))
            {
                _blockTemplateItems[blockTemplateItem.Guid] = blockTemplateItem;
            }
            else
            {
                _blockTemplateItems.Add(blockTemplateItem.Guid, blockTemplateItem);
            }
            SaveBlockTemplateItem();
        }

        public BlockTemplateItem GetBlockTemplateItem(Guid guid)
        {
            LoadBlockTemplateItems();
            if (_blockTemplateItems.ContainsKey(guid))
            {
                return _blockTemplateItems[guid];
            }
            else
            {
                return null;
            }
        }

        public void LoadBlockTemplateItems()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText("blockTemplateItems.json");

                _blockTemplateItems = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, BlockTemplateItem>>(json);
            }
            if (_blockTemplateItems == null)
            {
                _blockTemplateItems = new Dictionary<Guid, BlockTemplateItem>();
            }
            
        }

        public void SaveBlockTemplateItem()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_blockTemplateItems);

            string tempFile = "temp_blockTemplateItems.json";

            try
            {
                File.WriteAllText(tempFile, json);
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                File.Replace(tempFile, path, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
