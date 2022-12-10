using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLCrawler
{
    public class Node
    {
        private string firstTag;
        private string lastTag;
        private string data;
        private List<Node> childs;

        public Node()
        {
            childs = new List<Node>();
        }
        public Node CreateNewNode(string firstTag, string lastTag, string data)
        {
            Node node = new Node();
            node.firstTag = firstTag;
            node.lastTag = lastTag;
            node.data = data;
            return (node);
        }

        public string FirstTag { get { return this.firstTag; } }
        public string LastTag { get { return this.lastTag; } }  

        public string Data { get { return this.data; } }

        public List<Node> Childs { get { return childs; } }
    
        public void AddToChilds(Node node)
        {
            childs.Add(node);
        }
    }
}
