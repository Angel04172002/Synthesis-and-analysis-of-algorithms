using System;
using System.Linq;

namespace phonebook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var contacts = new Tuple<string, string>[100];
            int contactsCount = 0;
            bool run = true;

            while(run)
            {
                Console.Clear();
                Console.Write("a: add; f: find; q: quit: ");
                char key = Console.ReadKey(true).KeyChar;

                switch(key)
                {
                        case 'a':
                        {
                            if (contactsCount < contacts.Length)
                            {
                                Console.Clear();

                                Console.Write("Enter name: ");
                                string name = Console.ReadLine();
                                Console.Write("Enter num: ");
                                string num = Console.ReadLine();

                                Add(name, num, contacts, ref contactsCount);
                            }
                        }
                        
                        break;

                        case 'f':
                        {
                            Console.Clear();

                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();

                            int num = Find(name, contacts, ref contactsCount);

                            if(num != -1)
                            {
                                Console.Write($"{name} has num {contacts[num].Item2}");
                            }
                            else
                            {
                                Console.Write("This name does not exist in the phonebook.");
                            }
                            
                            Console.ReadLine();
                        }
                        break;

                        case 'q':
                        {
                            run = false;
                        }
                        break;
                }
            }
        }

        public static void Add(string name, string num, Tuple<string, string>[] contacts, ref int count)
        {
            contacts[count] = new Tuple<string, string>(name, num);
            count++;
        }

        public static int Find(string name, Tuple<string, string>[] contacts, ref int count)
        {
            for(int i = 0; i < count; i++)
            {
               
                if (contacts[i].Item1 == name)
                {
                    return i;
                }
                
            }

            return -1;
        }

        public static int FindBinary(string nameToFind, Tuple<string, string>[] contacts, int count)
        {
            int start = 0;
            int end = count - 1;

            while(start <= end)
            {
                int middle = (start + end) / 2;
                int num = String.Compare(contacts[middle].Item1, nameToFind);

                if(num < 0)
                {
                    start = middle + 1;
                }
                else if(num > 0)
                {
                    end = middle - 1;
                }
                else
                {
                    return middle;
                }
            }

            return -1;
        }

        private static void Insert(Tuple<string, string>[] contacts, string name, string num, ref int contactsCount)
        {
            var newContact = new Tuple<string, string>(name, num);
            var i = 0;

            for(; i < contactsCount; i++)
            {
                if (String.Compare(contacts[i].Item1, name) > 0)
                {
                    break;
                }
            }

            for(int k = contactsCount - 1; k >= i; k--)
            {
                contacts[k + 1] = contacts[k];
            }

            contacts[i] = newContact;
            contactsCount++;
        }
    }
}
