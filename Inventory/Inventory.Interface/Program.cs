using System;
using Inventory.DataModel.Repositories;
using Inventory.Classes.Enums;
using Inventory.ConsoleUI.Util;

namespace Inventory.ConsoleUI
{
    class Program
    {
        public const int MAX_NUM = 14;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your inventory! 'quit' or 'q' to exit");
            
            string input = "";
            while (input.ToLower() != "quit" && input.ToLower() != "q")
            {
                PrintOptions();
                input = Utility.ReadAndCheckForQuit();
                if (input.ToLower() == "q" || input.ToLower() == "quit")
                    continue;
                int selection;
                bool goodIn = int.TryParse(input, out selection);
                if (!goodIn || !(selection < MAX_NUM + 1 && selection > 0))
                {
                    Console.WriteLine($"Please enter a number 1-{MAX_NUM}");
                    continue;
                }
                HandleSelection(selection);
            }

        }



        private static void PrintOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) See all people");
            Console.WriteLine("2) See all cats");
            Console.WriteLine("3) Add person");
            Console.WriteLine("4) Add cat");
            Console.WriteLine("5) Update person");
            Console.WriteLine("6) Update cat");
            Console.WriteLine("7) Delete person/cat");
            Console.WriteLine("=====================");
            Console.WriteLine("8) See all stuff");
            Console.WriteLine("9) See all stuff by owner");
            Console.WriteLine("10) See all stuff by owner type");
            Console.WriteLine("11) Change owners");
            Console.WriteLine("12) Install/Uninstall stuff");
            Console.WriteLine("13) Update stuff");
            Console.WriteLine("14) Delete stuff");
            Console.WriteLine("15) Add stuff");
            Console.Write(":>");
        }

        private static void HandleSelection(int selection)
        {
            switch (selection)
            {
                case 1: OwnerConsoleUI.SeePeople();    break;
                case 2: OwnerConsoleUI.SeeCats();      break;
                case 3: OwnerConsoleUI.AddOwner(OwnerTypes.Human);    break;
                case 4: OwnerConsoleUI.AddOwner(OwnerTypes.Cat);       break;
                case 5: OwnerConsoleUI.UpdateOwner(OwnerTypes.Human); break;
                case 6: OwnerConsoleUI.UpdateOwner(OwnerTypes.Cat);    break;
                case 7: OwnerConsoleUI.DeleteOwner();  break;
                case 8: PossessionConsoleUI.SeeAllStuff(); break;
                case 9: PossessionConsoleUI.SeeAllStuffByOwner(); break;
                case 10: PossessionConsoleUI.SeeAllStuffByOwnerType(); break;
                case 11: PossessionConsoleUI.ChangeOwners(); break;
                case 12: PossessionConsoleUI.InstallUninstallStuff(); break;
                case 13: PossessionConsoleUI.UpdateStuff(); break;
                case 14: PossessionConsoleUI.DeleteStuff(); break;
                case 15: PossessionConsoleUI.AddStuff(); break;
                default: throw new ArgumentException();
            }
            Console.WriteLine("============================");

        }

        



  
    }
}
