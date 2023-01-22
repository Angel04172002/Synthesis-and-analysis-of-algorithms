using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLCrawler
{
    public class Node
    {
        private string firstTag;
        private string lastTag;
        private List<Node> allData;
        private bool isReadingAll;
        private List<Node> children;

        public Node(string firstTag, string lastTag, List<Node> allData, bool isReadingAll, string text)
        {
            this.firstTag = firstTag;
            this.lastTag = lastTag;
            this.allData = allData;
            this.isReadingAll = isReadingAll;
            this.Text = text;
            children = new List<Node>();
        }

        public string FirstTag { get { return this.firstTag; } }
        public string LastTag { get { return this.lastTag; } }  

        public string Text { get; set; }

        public bool IsReadingAll { get { return this.isReadingAll; } }  

        public List<Node> Children { get { return this.children; } }

        public Node Father { get; set; }

        public string Id { get; set; }
    
    }
}
