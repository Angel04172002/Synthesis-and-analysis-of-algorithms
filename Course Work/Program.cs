sing System;
using System.Collections.Generic;

namespace HTMLCrawler
{
    public class Program
    {
        static void Main(string[] args)
        {
        

            Tree tree = new Tree();

            string document = "";
            string line = Console.ReadLine();

            while(line != "")
            {
                document += $"{line}\n";
                line = Console.ReadLine();  
            }

            string[] result = CustomString.Split(document, "\n");


            List<string> firstTags = new List<string>();


            Node previousNode = null;
            Node mostPreviousNode = null;
            

            for(int i = 0; i < result.Length; i++)  
            {
                string row = result[i];

                if (tree.Nodes.Exists(n => n.LastTag == row))
                {
                    continue;
                }

                string firstTag = GetFirstHtmlTag(row);
                firstTags.Add(firstTag);

                string textForLastTag = firstTag;
                string lastTag = GetLastHtmlTag(textForLastTag);

                Node node = new Node();
                Node newNode = node.CreateNewNode(firstTag, lastTag, row);
           
                if (i >= 1 && newNode.FirstTag != previousNode.FirstTag)
                {

                    if (newNode.FirstTag == "<html>" || newNode.FirstTag == "<head>" || newNode.FirstTag == "<body>")
                    {
                        tree.AddNode(newNode);
                    }
                    
                    string previousRow = result[i - 1];
                    string previousFirstTag = GetFirstHtmlTag(previousRow);
                    string previousTextForLastTag = previousFirstTag;
                    string previousLastTag = GetLastHtmlTag(previousTextForLastTag);
                    Node node2 = new Node();
                    previousNode = node2.CreateNewNode(previousFirstTag, previousLastTag, previousRow);
                }



                if (i != 0)
                {
                    string nextRow = result[i + 1];
                    string nextFirstTag = GetFirstHtmlTag(nextRow);
                    string nextTextForLastTag = nextFirstTag;   
                    string nextLastTag = GetLastHtmlTag(nextTextForLastTag);    
                    Node nextNode = new Node();
                    Node nextNewNode = nextNode.CreateNewNode(nextFirstTag, nextLastTag, nextRow);


                    if (mostPreviousNode.FirstTag != previousNode.FirstTag)
                    {

                        if (firstTags.Contains(nextNewNode.FirstTag))
                        { 
                            mostPreviousNode = previousNode;
                            mostPreviousNode.AddToChilds(newNode);
                            tree.AddNode(mostPreviousNode);
                        }
                        else
                        {
                            mostPreviousNode.AddToChilds(newNode);
                        }
                    }
                    

                    if(mostPreviousNode.FirstTag == "<html>")
                    {
                        mostPreviousNode.AddToChilds(newNode);
                    }

                    if (mostPreviousNode.FirstTag == "<head>")
                    {
                        tree.Nodes[0].AddToChilds(newNode);
                    }


                }
                
                if (i <= 1)
                {
              
                    tree.AddNode(newNode);
                }


                if(i == 0)
                {
                    previousNode = newNode;
                    mostPreviousNode = previousNode;
                }
            }
        }
        
        public static string GetLastHtmlTag(string text)
        {
            //text = "<body> => tag = "</body>"

            string[] symbols = CustomString.Split(text, String.Empty);
            symbols = CustomString.Insert(symbols, "/", 1);
            string tag = CustomString.Join(symbols, "");

            return tag;

        }

        public static string GetFirstHtmlTag(string text)
        {
            //"<p>ANGEL</p>" => <p>
            string result = "";

            int i = 0;

            while (text[i] != '>')
            {
                if (text[i] != (char)47)
                {
                    result += text[i];
                    i++;
                }
                else
                {
                    i++;
                }
            }

            result += '>';

            return result;
        }
    }
}



                    
                    
                    
                    
                    
