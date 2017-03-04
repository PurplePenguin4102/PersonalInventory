using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.Interface.Util;

namespace Inventory.Interface
{
    public static class TextParser
    {
        public static bool MakeSelection<T>(out T obj, string msg, List<T> db) 
            where T : IContainsId
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
            obj = db.FirstOrDefault(g => g.Id == id);
            return obj != null;
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
