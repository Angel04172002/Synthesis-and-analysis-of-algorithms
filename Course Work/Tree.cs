using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLCrawler
{
    public class Tree
    {
        private List<Node> nodes;

        public Tree()
        {
            nodes = new List<Node>();
        }

        public List<Node> Nodes
        {
            get
            {
                return this.nodes;
            }
        }

        public void AddNode(Node node)
        {
            if(nodes.Exists(n => n.Data == node.Data))
            {
                int index = nodes.FindIndex(n => n.Data == node.Data);
                nodes[index] = node;
                return;
            }

            nodes.Add(node);
        }
    }
}
