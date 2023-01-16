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
        private List<Node> childs;
  


        public Node(string firstTag, string lastTag, List<Node> allData, bool isReadingAll, string text)
        {
            this.firstTag = firstTag;
            this.lastTag = lastTag;
            this.allData = allData;
            this.isReadingAll = isReadingAll;
            this.Text = text;
            childs = new List<Node>();
        }

        public string FirstTag { get { return this.firstTag; } }
        public string LastTag { get { return this.lastTag; } }  

        public string Text { get; set; }

        public List<Node> Data { get { return this.allData; } }

        public bool IsReadingAll { get { return this.isReadingAll; } }  

        public List<Node> Childs { get { return this.childs; } }
    
    }
}
