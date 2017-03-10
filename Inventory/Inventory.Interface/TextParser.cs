using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.ConsoleUI.Util;

namespace Inventory.ConsoleUI
{
    public static class TextParser
    {
        public static Option SelectItemFromList(string msg, List<Option> db) 
        {
            string input;
            int id = -1;
            while (id < 1)
            {
                Console.Write(msg);
                input = Utility.ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            Option obj = db.FirstOrDefault(g => g.Id == id);
            return obj;
        }

        internal static int MakeSelection(Type type)
        {
            int i = 0;
            Console.WriteLine("Please Choose : ");
            foreach (var value in type.GetEnumValues())
            {
                Console.WriteLine($"{i}) {value}");
                i++;
            }
            int id = -1;
            string input = "";
            while (!int.TryParse(input, out id))
            {
                input = Utility.ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            return id;
        }
    }
}
