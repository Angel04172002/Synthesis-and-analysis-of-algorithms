using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTMLCrawler
{
    public static class CustomString
    {
        public static string[] SplitByChar(string text, string separator)
        {

            if(separator == "")
            {
                List<string> symbols = new List<string>();

                foreach(var symbol in text)
                {
                    symbols.Add(symbol.ToString());
                }

                return CustomString.ToArray(symbols);
            }


            int delimCount = 0;

            foreach(var ch in text)
            {
                if(CustomString.Contains(ch.ToString(), separator))
                {
                    delimCount++;
                }
            }

            string[] result = new string[delimCount + 1];

            result[0] = "";
            int i = 0;

            foreach(var ch in text)
            {
                if(!CustomString.Contains(ch.ToString(), separator))
                {
                    result[i] += ch;
                }
                else
                {
                    ++i;
                    result[i] = "";
                }
            }

            return result;
        }

        public static string Join(string[] symbols, string separator)
        {
            string result = "";

            for(int i = 0; i < symbols.Length; i++)
            {
                if(i == symbols.Length - 1)
                {
                    result += symbols[i];
                    continue;
                }
                    
                result += symbols[i];
                result += separator;
            }

            return result;
        }

        public static string[] Insert(string[] elements, string element, int index)
        {
            string[] newElements = new string[elements.Length + 1];

            for(int i = 0; i < elements.Length; i++)
            {
                newElements[i] = elements[i];
            }

          
            for(int i = newElements.Length - 1; i > index; i--)
            {
                newElements[i] = newElements[i - 1];
            }

            newElements[index] = element;
            return newElements;
        }
public static List<Node> InsertInListOfNodes(List<Node> nodes, Node element, int index)
        {
            List<Node> newNodes = new List<Node>(); 

            for (int i = 0; i < nodes.Count; i++)
            {
                newNodes[i] = nodes[i];
            }

            for (int i = newNodes.Count - 1; i > index; i--)
            {
                newNodes[i] = newNodes[i - 1];
            }

            newNodes[index] = element;
            return newNodes;
        }

        public static bool Contains(string text, string element)
        {
            bool isContaining = true;
            int m = 0;

            for(int i = 0; i < text.Length; i++)
            {
                if (text[i] == element[m])
                {
                    if(m == element.Length - 1)
                    {
                        return isContaining;
                    }

                    m++;
                }
                else
                {
                    m = 0;
                }
            }

            isContaining = false;
            return isContaining;
        }


        public static bool ContainsInArray(string[] elements, string element)
        {
            for(int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == element)
                {
                    return true;
                }
            }

            return false;
        }
 public static bool Exists(List<Node> nodes2, Predicate<Node> match)
        {
            for(int i = 0; i < nodes2.Count; i++)
            {
                if (match(nodes2[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static int FindIndex(List<Node> nodes, Predicate<Node> match)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (match(nodes[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static string[] SplitByString(string text, string separator)
        {

            List<string> result = new List<string>();
            int i = 0;
            string stringToAdd = "";
            char currChar = separator[i];
      

            for (int k = 0; k < text.Length; k++)
            {
                char currSymbol = text[k];
         

                if (text[k] != currChar)
                {
                    int comparerChar;
                    int index = CustomString.IndexOf(text, separator[0].ToString(), k + 3);

                    if(index == -1)
                    {
                        comparerChar = text.Length - 1;
                    }
                    else
                    {
                        comparerChar = index - 1;
                    }

                    while (i != comparerChar)
                    {
                      
                        currSymbol = text[i];
                        ++i;
                        stringToAdd += currSymbol;
                    }

                    result.Add(stringToAdd);
                }

                stringToAdd = "";
                k = i;
                i += separator.Length;
                i++;



                if (k + separator.Length + 1 < text.Length)
                {
                    if (CustomString.IndexOf(text, separator, k + separator.Length + 1) == -1)
                    {
                        currChar = text[text.Length - 1];
                    }
                }
                
            }

            return CustomString.ToArray(result);
        }
 public static int IndexOf(string text, string separator)
        {
            for(int i = 0; i < text.Length; i++)
            {
                if (text[i].ToString() == separator)
                {
                    return i;
                }
            }

            return -1;
        }


        public static int IndexOf(string text, string separator, int start)
        {
            for(int i = start; i < text.Length; i++)
            {
                if (text[i].ToString() == separator)
                {
                    return i;
                }
            }

            return -1;
        }


        public static string[] ToArray(List<string> symbols)
        {
            string[] stringsArr = new string[symbols.Count];

            for(int i = 0; i < symbols.Count; i++)
            {
                stringsArr[i] = symbols[i];
            }

            return stringsArr;
        }

    }
}
