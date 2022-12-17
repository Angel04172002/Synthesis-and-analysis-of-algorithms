using System;
using System.Collections.Generic;

namespace HTMLCrawler
{
    public class Program
    {
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

            string[] result = CustomString.Split(document, "\n");


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

                if (firstTag != lastTag)
                {
                    tree.Push(node);
                }

                if (firstTag == lastTag)
                {
                    Node mainNode = null;

                    foreach (var item in tree)
                    {
                        if (item.LastTag == node.LastTag)
                        {
                            mainNode = item;
                            break;
                        }
                    }


                    if (mainNode != null && isReadingAll)
                    {

                        mainNode.Data.Add(mainNode);
                        int v = 0;

                        while (tree.Peek().LastTag != node.LastTag)
                        {
                            Node nextNode = tree.Pop();
                            mainNode.Data.Insert(v, nextNode);
                            mainNode.Childs.Insert(v, nextNode);

                        }
                    }
                }


                if (isReadingAll == false)
                {
                    node.Data.Add(node);
                }
            }
            string command = Console.ReadLine();

            while (command != "END")
            {
                string[] tokens = CustomString.Split(command, " ");

                if (tokens[0] == "PRINT")
                {
                    if (tokens[1] == "//")
                    {
                        Console.WriteLine(wholeText);
                    }
                    else
                    {

                        string[] tags = CustomString.Split(tokens[1], "/");
                        string firstTag = "";


                        for (int k = 0; k < tags[3].Length; k++)
                        {
                            firstTag += tags[3][k];
                        }

                        tags[3] = firstTag;

                   
                        FindByPath(tree, tree.Peek(), wholeText, tags, 5);
                    }
                }

                command = Console.ReadLine();
            }


        }




        public static void FindByPath(Stack<Node> tree, Node node, string wholeText, string[] tags, int ka, bool isFirst = true)
        {

            int m = 3;

            var nodes = new List<Node>();
            string currentTag = "";

            if (node.FirstTag == "<html>")
            {
                currentTag = tags[m];
                nodes = node.Childs;
            }
            else
            {
                nodes.Add(node);
                node = nodes[0];
                currentTag = node.FirstTag;
            }

            if (currentTag != node.FirstTag)
            {
                string[] symbols = CustomString.Split(currentTag, "");
                symbols = CustomString.Insert(symbols, "<", 0);
                symbols = CustomString.Insert(symbols, ">", symbols.Length);
                currentTag = CustomString.Join(symbols, "");
            }

            var index = CustomString.FindIndex(nodes, n => n.FirstTag == currentTag);


            if (index != -1 && nodes[index].Childs.Count > 0)
            {
                nodes = nodes[index].Childs;
            }
            else if(index != -1 && nodes[index].Childs.Count == 0)
            {
                foreach(var z in nodes)
                {
                    Console.WriteLine(z.Text);
                }

                return;
            }
 while (index != -1)
            {
                m++;

                if (node.FirstTag == "<html>")
                {
                    currentTag = tags[m];
                }
                else
                {
                    if (nodes.Count > 0)
                    {
                        node = nodes[index];
                        currentTag = tags[CustomString.FindIndex(nodes, n => n.FirstTag == node.FirstTag) + 6];
                    }
                }

                if (nodes.Count > 0)
                {
                    string[] symbols = CustomString.Split(currentTag, "");
                    symbols = CustomString.Insert(symbols, "<", 0);
                    symbols = CustomString.Insert(symbols, ">", symbols.Length);
                    currentTag = CustomString.Join(symbols, "");
                }



                if (nodes.Count >= 1)
                {
                    foreach (var n in nodes)
                    {
                        if (isFirst == false && ka < tags.Length - 1)
                        {
                            ka++;
                        }

                        FindByPath(tree, n, wholeText, tags, ka, false);
                    }

                    break;
                }


                index = CustomString.FindIndex(nodes, n => n.FirstTag == currentTag);


                if (index != -1 && nodes[index].Childs.Count > 0)
                {
                    nodes = nodes[index].Childs;
                }
                else
                {
                    foreach(var n in nodes)
                    {
                        if (n.Text != String.Empty)
                        {
                            Console.WriteLine(n.Text);
                        }
                    }

                    return;
                }

            }

            /*for (int c = 0; c < nodes.Count; c++)
            {
                if (c != nodes.Count - 1)
                {
                    Console.Write($"{nodes[c].Text}, ");
                }
                else
                {
                    Console.Write($"{nodes[c].Text}");
                }
            }*/

        }
    


 public static string GetLastHtmlTag(string text)
    {
        //text = "<body> => tag = "</body>"

        string[] symbols = CustomString.Split(text, String.Empty);

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

        for (int i = 0; i < text.Length; i++)
        {
            if (i != 0 && text[i - 1] == '>')
            {
                while (text[i] != '<')
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
        //"<p>ANGEL</p>" => <p>
        string result = "";

        int i = 0;

        while (text[i] != '>')
        {
            result += text[i];
            i++;
        }

        result += '>';

        return result;
    }


        public static string MakeFirstHtmlTagFromText(string text)
        {

            string[] symbols = CustomString.Split(text, "");
            symbols = CustomString.Insert(symbols, "<", 0);
            symbols = CustomString.Insert(symbols, ">", symbols.Length);
            text = CustomString.Join(symbols, "");

            return text;

        }
    }
}

