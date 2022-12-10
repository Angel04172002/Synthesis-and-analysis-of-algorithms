using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLCrawler
{
    public static class CustomString
    {
        public static string[] Split(string text, string separator)
        {

            if(separator == "")
            {
                List<string> symbols = new List<string>();

                foreach(var symbol in text)
                {
                    symbols.Add(symbol.ToString());
                }

                return symbols.ToArray();
            }


            int delimCount = 0;

            foreach(var ch in text)
            {
                if(ch.ToString() == separator)
                {
                    delimCount++;
                }
            }

            string[] result = new string[delimCount + 1];

            result[0] = "";
            int i = 0;

            foreach(var ch in text)
            {
                if(ch.ToString() != separator)
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
    }
}

