using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class AttributeTreeManager
    {
        private Dictionary<Guid, AttributeNode> _trees = new Dictionary<Guid, AttributeNode>();
        string path = "attributeTrees.json";

        public void AddNewTree(AttributeNode tree)
        {
            LoadTrees();
            _trees.Add(tree.Guid, tree);
            SaveTrees();
        }

        public AttributeNode GetAttributeTree(Guid guid)
        {
            LoadTrees();
            if (_trees.TryGetValue(guid, out AttributeNode tree))
            {
                return tree;
            }
            return null;
        }

        public void LoadTrees()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText("attributeTrees.json");

                _trees = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<Guid, AttributeNode>>(json);
            }
            if (_trees == null)
            {
                _trees = new Dictionary<Guid, AttributeNode>();
            }
            
        }

        public void SaveTrees()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_trees);

            string tempFile = "temp_attributeTrees.json";

            string actualFile = "attributeTrees.json";

            try
            {
                File.WriteAllText(tempFile, json);
                if (!File.Exists(actualFile))
                {
                    using (File.Create(actualFile))
                    {

                    }
                }
                File.Replace(tempFile, actualFile, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
