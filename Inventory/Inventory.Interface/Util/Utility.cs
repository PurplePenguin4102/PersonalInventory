using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI.Util
{
    public static class Utility
    {
        public static string ReadAndCheckForQuit()
        {
            string input = Console.ReadLine();
            if (input == "q" || input == "quit")
                Environment.Exit(0);
            return input;
        }
    }
}
