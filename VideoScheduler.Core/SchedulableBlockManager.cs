using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    //TODO this class isn't being used yet, but the idea is to define a block that can be referenced for re-use
    public class SchedulableBlockManager
    {
        private Dictionary<Guid, SchedulableBlock> _schedulableBlocks = new Dictionary<Guid, SchedulableBlock>();
        private string path = "schedulableBlocks.json";

        public void AddOrUpdateSchedulableBlock(SchedulableBlock schedulableBlock)
        {
            LoadBlocks();
            if (_schedulableBlocks.ContainsKey(schedulableBlock.Guid))
            {
                _schedulableBlocks[schedulableBlock.Guid] = schedulableBlock;
            }
            else
            {
                _schedulableBlocks.Add(schedulableBlock.Guid, schedulableBlock);
            }
            SaveBlocks();
        }

        public SchedulableBlock GetSchedulableBlock(Guid guid)
        {
            LoadBlocks();
            if (_schedulableBlocks.ContainsKey(guid))
            {
                return _schedulableBlocks[guid];
            }
            else
            {
                return null;
            }
        }

        public void LoadBlocks()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText("schedulableBlocks.json");

                _schedulableBlocks = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, SchedulableBlock>>(json);
            }
            if (_schedulableBlocks == null)
            {
                _schedulableBlocks = new Dictionary<Guid, SchedulableBlock>();
            }   
        }

        public void SaveBlocks()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_schedulableBlocks);

            string tempFile = "temp_schedulableBlocks.json";

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
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
