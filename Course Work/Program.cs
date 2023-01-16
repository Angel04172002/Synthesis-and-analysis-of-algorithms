using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
                        string finalTag = "";
                        char index = '0';
                        string[] tags = CustomString.Split(tokens[1], "/");


                        if (CustomString.Contains(tags[tags.Length - 1], "[") 
                            && CustomString.Contains(tags[tags.Length - 1], "]"))
                        {
                            finalTag = tags[tags.Length - 1];
                            index = (finalTag[finalTag.Length - 2]);
                            FindByPath(tree, tree.Peek(), wholeText, tags, 5, true, int.Parse(index.ToString()), finalTag);
                            command = Console.ReadLine();
                            continue;
                        }



                        string firstTag = "";


                        for (int k = 0; k < tags[3].Length; k++)
                        {
                            firstTag += tags[3][k];
                        }

                        tags[3] = firstTag;

                   
                        FindByPath(tree, tree.Peek(), wholeText, tags, 5);
                    }
                }

                else if (tokens[0] == "SET")
                {
                    string[] tags = CustomString.Split(tokens[1], "/");
                    string replacedText = tokens[2];
                    string finalTag = tags[tags.Length - 1];
            
                    SetNewText(tree, tree.Peek(), tags, replacedText, 5, true, finalTag);
                }

                command = Console.ReadLine();
            }


        }
 public static void SetNewText(Stack<Node> tree, Node node, string[] tags, string replacedText
            , int ka, bool isFirst = true,  string finalTag = "")
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
                        currentTag = tags[CustomString.FindIndex(nodes, n => n.FirstTag == node.FirstTag) + ka];

                    }
                }

                if (nodes.Count > 0)
                {
                    string[] symbols = CustomString.Split(currentTag, "");
                    symbols = CustomString.Insert(symbols, "<", 0);
                    symbols = CustomString.Insert(symbols, ">", symbols.Length);
                    currentTag = CustomString.Join(symbols, "");


                    if(currentTag == MakeFirstHtmlTagFromText(tags[tags.Length - 1]))
                    {
                        //SET
                    }

                }



                if (nodes.Count > 1)
                {
                    int g = 0;

                    foreach (var n in nodes)
                    {
                        if (isFirst == false && ka < tags.Length - 1)
                        {
                            ka++;
                        }

                        string newFirstHtmlTag = MakeFirstHtmlTagFromText(tags[m + 1]);

                        if (CustomString.Exists(nodes[g].Childs, n => n.FirstTag == newFirstHtmlTag))
                        {
                              SetNewText(tree, n, tags, replacedText, ka, false,  finalTag);
                              continue;
                            


                        }

                        g++;
                    }
 if (nodes.All(n => n.Childs.Count == 0))
                    {
                        break;
                    }
                }


                index = CustomString.FindIndex(nodes, n => n.FirstTag == currentTag);


                if (index != -1 && nodes[index].Childs.Count > 0)
                {
                    nodes = nodes[index].Childs;

                    if (ka < tags.Length - 1)
                    {
                        ka++;
                    }
                }
           

            }
        }

public static void FindByPath(Stack<Node> tree, Node node, string wholeText, string[] tags, int ka, bool isFirst = true
            , int indexFinal = 0, string finalTag = "")
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

            if(index != -1 && nodes[index].Childs.Count == 0)
            {
                for(int x = 0; x < nodes.Count; x++)
                {
                    if (indexFinal == 0 && nodes[x].Text != String.Empty)
                    {
                        Console.WriteLine(nodes[x].Text);
                    }

                    if(indexFinal == x + 1)
                    {
                        Console.WriteLine(nodes[x].Text);
                    }

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
                        currentTag = tags[CustomString.FindIndex(nodes, n => n.FirstTag == node.FirstTag) + ka];

                    }
                }

                if (nodes.Count > 0)
                {
                    string[] symbols = CustomString.Split(currentTag, "");
                    symbols = CustomString.Insert(symbols, "<", 0);
                    symbols = CustomString.Insert(symbols, ">", symbols.Length);
                    currentTag = CustomString.Join(symbols, "");


                    if (currentTag.Contains('[') && currentTag.Contains(']'))
                    {
                        currentTag = currentTag.Substring(1, currentTag.Length - 2);
                        tags[tags.Length - 1] = currentTag;

                        for (int s = 0; s < nodes.Count; s++)
                        {
                            if (finalTag != String.Empty)
                            {
                                if (indexFinal == s + 1)
                                {
                                    Console.WriteLine(nodes[s].Text);
                                    break;
                                }

                                continue;
                            }
                        }

                        return;
                    }    

                }



                if (nodes.Count > 1)
                {
                    int g = 0;

                    foreach (var n in nodes)
                    {
                        if (isFirst == false && ka < tags.Length - 1)
                        {
                            ka++;
                        }

                        string newFirstHtmlTag = MakeFirstHtmlTagFromText(tags[m + 1]);

                        if (CustomString.Exists(nodes[g].Childs, n => n.FirstTag == newFirstHtmlTag))
                        {
                            if(indexFinal != 0)
                            {
                                FindByPath(tree, n, wholeText, tags, ka, false, indexFinal, finalTag);
                                continue;
                            }

                            FindByPath(tree, n, wholeText, tags, ka, false);

                        }

                        g++;
                    }
                     if(indexFinal != 0)
                    {
                        break;
                    }

                    if (nodes.All(n => n.Childs.Count == 0))
                    {
                        break;
                    }
                }


                index = CustomString.FindIndex(nodes, n => n.FirstTag == currentTag);


                if (index != -1 && nodes[index].Childs.Count > 0)
                {
                    nodes = nodes[index].Childs;

                    if (ka < tags.Length - 1)
                    {
                        ka++;
                    }
                }
                else
                {
                    foreach (var n in nodes)
                    {
                        if (n.Text != String.Empty)
                        {
                            Console.WriteLine(n.Text);
                        }
                    }

                    return;
                }

            }
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
            string substring = "";
            string[] symbols = CustomString.Split(text, "");


            if (symbols.Contains("[") && symbols.Contains("]"))
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




