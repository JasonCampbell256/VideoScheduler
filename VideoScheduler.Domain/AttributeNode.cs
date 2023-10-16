using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoScheduler.Domain
{
    public class AttributeNode : ISchedulableContent
    {
        [Newtonsoft.Json.JsonProperty]
        public Guid Guid { get; set; }
        [Newtonsoft.Json.JsonProperty]
        public string Value { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public AttributeNode Parent { get; set; }
        [Newtonsoft.Json.JsonProperty]
        public AttributeNode Child { get; set; }

        [Newtonsoft.Json.JsonProperty]
        public string Description { get; set; }

        public AttributeNode()
        {
        }

        public AttributeNode(string value)
        {
            Guid = Guid.NewGuid();
            Value = value;
        }

        public AttributeNode(List<string> attributeList)
        {
            if (attributeList == null || attributeList.Count == 0)
            {
                throw new ArgumentException("The attribute list cannot be null or empty.");
            }

            Value = attributeList[0];
            AttributeNode current = this;
            current.Guid = Guid.NewGuid();

            for (int i = 1; i < attributeList.Count; ++i)
            {
                current.Child = new AttributeNode(attributeList[i]);
                current = current.Child;
            }
        }

        public static AttributeNode AddAttribute(AttributeNode node, string value)
        {
            if (node == null)
            {
                node = new AttributeNode(value);
                node.Value = value;
            }
            else
            {
                if (node.Value == value)
                {
                    return node;
                }
                else
                {
                    if (node.Child == null)
                    {
                        node.Child = new AttributeNode(value);
                        node.Child.Value = value;
                        node.Child.Parent = node;
                    }
                    else
                    {
                        AddAttribute(node.Child, value);
                    }
                }
            }

            return node;
        }
    }
}
