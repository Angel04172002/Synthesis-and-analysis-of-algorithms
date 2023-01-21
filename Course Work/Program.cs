using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace HTMLCrawler
{
    public class Program
    {

    


        private static Node firstNode;
        static void Main(string[] args)
        {

            Stack<Node> tree = new Stack<Node>();

            string wholeText = "";
            string document = "";
            string line = Console.ReadLine();
            

            while (line != "")
            {
                document += $"{line}\n";
                line = Console.ReadLine();
            }

            string[] result = CustomString.SplitByChar(document, "\n");


            for (int i = 0; i < result.Length - 1; i++)
            {
                string row = result[i];
                
                string firstTag = GetFirstHtmlTag(row);
                string lastTag = GetLastHtmlTag(firstTag);
                string data = GetData(row, firstTag, lastTag);
                bool isReadingAll = false;


          
                if (data != String.Empty)
                {
                    wholeText += $"{data}\n";
                }


                if (data == String.Empty)
                {
                    isReadingAll = true;
                }


                Node node = new Node(firstTag, lastTag, new List<Node>(), isReadingAll, data);

                if (CustomString.Contains(row, "id="))
                {
                    string[] idTokens = CustomString.SplitByString(row, "id=");
                    string[] idTokens2 = CustomString.SplitByChar(idTokens[1], ">");

                    node.Id = idTokens2[0];
                }


                if (firstTag != lastTag)
                {
                    tree.Push(node);
                }

                if (firstTag == lastTag)
                {
                    Node mainNode = null;

                    foreach (var item in tree)
                    {
                        if (item.LastTag == node.LastTag && item.Children.Count == 0)
                        {
                            mainNode = item;
                            break;
                        }
                    }
if (mainNode != null && isReadingAll)
                    {

                       
                        int v = 0;

                        while (tree.Peek() != mainNode)
                        {
                            Node nextNode = tree.Pop();
                            //CustomString.InsertInListOfNodes(mainNode.Childs, nextNode, v);
                            mainNode.Children.Insert(v, nextNode);
                          
                        }

                        
                    } 
                }   
            }


            
            string command = Console.ReadLine();

            while (command != "END")
            {
                string[] tokens = CustomString.SplitByChar(command, " ");

                if (tokens[0] == "PRINT")
                {
                    if (tokens[1] == "//")
                    {
                        Console.WriteLine(wholeText);
                    }
                    else
                    {
                        string finalTag = "";
                        char index = '0';
                        string[] tags = CustomString.SplitByChar(tokens[1], "/");
                        string firstTag = tags[tags.Length - 1];
                        string secondFinalTag = tags[tags.Length - 2];


                        if (CustomString.Contains(tags[tags.Length - 1], "[") 
                            && CustomString.Contains(tags[tags.Length - 1], "]"))
                        {

                            if (CustomString.Contains(tags[tags.Length - 1], "@"))
                            {
                                string[] tokens2 = CustomString.SplitByChar(tags[tags.Length - 1], "[");
                                string tag = tokens2[0];
                                string[] tokens3 = CustomString.SplitByChar(tokens2[1], "=");
                                string idName = tokens3[1].Substring(0, tokens3[1].Length - 1);
                                FindByPath(tree, tree.Peek(), tag, secondFinalTag, index, idName);

                                command = Console.ReadLine();
                                continue;
                            }


                            finalTag = tags[tags.Length - 1];
                            index = (finalTag[finalTag.Length - 2]);

                            FindByPath(tree, tree.Peek(), finalTag, secondFinalTag, int.Parse(index.ToString()));
                       
                            command = Console.ReadLine();
                            continue;
                        }

        FindByPath(tree, tree.Peek(),  firstTag, secondFinalTag);
                    }
                }

                else if (tokens[0] == "SET")
                {
                    string[] tags = CustomString.SplitByChar(tokens[1], "/");
                    string replacedText = tokens[2];
                    string finalTag = tags[tags.Length - 1];
                    string secondFinalTag = tags[tags.Length - 2];

                    SetNewText(tree, replacedText, finalTag, secondFinalTag, tree.Peek());
                }

                command = Console.ReadLine();
            }


        }
 public static void SetNewText(Stack<Node> tree, string replacedText, string finalTag, string secondToFinalTag, Node node, int index = 0)
        {

            if(node.FirstTag == MakeFirstHtmlTagFromText(finalTag) && node.FirstTag == MakeFirstHtmlTagFromText(secondToFinalTag))
            {
                node.Text = replacedText;
            }

            for(int i = 0; i < node.Children.Count; i++)
            {
                if (node.Children[i].FirstTag == MakeFirstHtmlTagFromText(finalTag) && node.FirstTag == MakeFirstHtmlTagFromText(secondToFinalTag))
                {
                    if (CustomString.Contains(finalTag, "[") && CustomString.Contains(finalTag, "]"))
                    {
                        if (i + 1 == index)
                        {
                            node.Children[i].Text = replacedText;
                        }
                    }
                    else
                    {
                        node.Children[i].Text = replacedText;
                    }
                }
                else
                {
                    SetNewText(tree, replacedText, finalTag, secondToFinalTag, node.Children[i]);
                }
                 
            }

            return;
                      
        }
 public static void FindByPath(Stack<Node> tree, Node node, string finalTag, string secondToFinalTag, int index = 0, string id = "")
        {
            
            //if (node.FirstTag == MakeFirstHtmlTagFromText(finalTag) && node.FirstTag == MakeFirstHtmlTagFromText(secondToFinalTag))
            //{
              //  Console.WriteLine(node.Text);
            //}

            if(finalTag == "*" && MakeFirstHtmlTagFromText(secondToFinalTag) == node.FirstTag)
            {
                if(node.Children.Count > 0)
                {
                    for(int i = 0; i < node.Children.Count; i++)
                    {
                        PrintTextChildNode(node.Children[i]);

                    }
                }

                return;
            }


            for(int i = 0; i < node.Children.Count; i++)
            {

                if (node.Children[i].FirstTag == MakeFirstHtmlTagFromText(finalTag) && node.FirstTag == MakeFirstHtmlTagFromText(secondToFinalTag))
                {

                    if (CustomString.Contains(finalTag, "[") && CustomString.Contains(finalTag, "]"))
                    {
                        if(i + 1 == index)
                        {
                            PrintWholeRows(node.Children[i], true);
                        }
                    }

                    else if (node.Children[i].Id == id)
                    {
                        Console.WriteLine(node.Children[i].Text);
                    }

                    else if (node.Children[i].Text == String.Empty)
                    {
                        firstNode = node.Children[i];
                        PrintWholeRows(node.Children[i], false);
                    }

                    else
                    {
                        if (id == String.Empty)
                        {

                            Console.WriteLine(node.Children[i].Text);
                        }
                    }
                }
                else
                {
                    FindByPath(tree, node.Children[i], finalTag, secondToFinalTag, index, id);
                }
            }

            return;

        }


        public static void PrintWholeRows(Node node, bool isExactParagraph)
        {

            int k = 0;

            if (node.Text != String.Empty && isExactParagraph)
            {
                Console.WriteLine(node.Text);
            }

          foreach(var child in node.Children)
          {
              Console.Write(child.FirstTag);


                if (child.Text != String.Empty)
                {
                    Console.Write(child.Text);
                    Console.Write(child.LastTag);
                }

                else if (child.Children.Count > 0)
                {
                    foreach (var c in child.Children)
                    {
                        Console.WriteLine();
                        Console.Write(c.FirstTag);
                        Console.Write(c.Text);
                        Console.Write(c.LastTag);
                        Console.WriteLine();
                    }
                }

                k++;

                if (child.Text == String.Empty && node.Text == String.Empty)
                {

                    Console.WriteLine(node.LastTag);
                }

                if (node.Children.Count > k)
                {
                    PrintWholeRows(node.Children[k], isExactParagraph);
                }
          }

            if (firstNode != null && node.LastTag == firstNode.LastTag)
            {
                Console.WriteLine();
                Console.WriteLine(node.LastTag);
            }

        }
        public static void PrintTextChildNode(Node node)
    { 
        if (node.Text != String.Empty)
        { 
            Console.WriteLine(node.Text);
        }

        foreach(var child in node.Children)
        {
            PrintTextChildNode(child);
        }    
    }



    public static string GetLastHtmlTag(string text)
    {
        //text = "<body> => tag = "</body>"

        string[] symbols = CustomString.SplitByChar(text, String.Empty);

        if (symbols[1] != "/")
        {
            symbols = CustomString.Insert(symbols, "/", 1);
        }

        string tag = CustomString.Join(symbols, "");

        return tag;

    }

    public static string GetData(string text, string firstTag, string lastTag)
    {
        string result = "";
        char endSymbol = '<';

        for (int i = 0; i < text.Length; i++)
        {
            if (i != 0 && text[i - 1] == '>')
            {
                while (text[i] != endSymbol)
                {
                    result += text[i];
                    ++i;
                }

                break;
            }
        }

        return result;
    }

    public static string GetFirstHtmlTag(string text)
    {
          
            char endSymbol = '>';

            if (CustomString.Contains(text, "id="))
            {
                endSymbol = ' ';
            }




        string result = "";

        int i = 0;

        while (text[i] != endSymbol)
        {
            if(i == 0 && endSymbol == '[')
            {
                result += '<';
            }

            result += text[i];
            i++;
        }

        result += '>';

        return result;
    }

public static string MakeFirstHtmlTagFromText(string text)
        {
            string substring = "";
            string[] symbols = CustomString.SplitByChar(text, "");


            if (CustomString.ContainsInArray(symbols, "[") && CustomString.ContainsInArray(symbols, "["))
            {
                substring += "<";
                int i = 0;

                while (symbols[i] != "[")
                {
                    substring += symbols[i];
                    ++i;
                }

                substring += ">";
                return substring;
            }



            symbols = CustomString.Insert(symbols, "<", 0);
            symbols = CustomString.Insert(symbols, ">", symbols.Length);

      

            text = CustomString.Join(symbols, "");

            return text;

        }
    }
}

