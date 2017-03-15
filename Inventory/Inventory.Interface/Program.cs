using System;
using Inventory.DataModel;
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
            InventoryDatabase DB = new InventoryDatabase();
            OwnerConsoleUI ownerUI = new OwnerConsoleUI(DB.Owners);
            PossessionConsoleUI possessionUI = new PossessionConsoleUI(DB.Possessions, DB.Owners);
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
                HandleSelection(selection, ownerUI, possessionUI);
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

        private static void HandleSelection(int selection, OwnerConsoleUI ownerUI, PossessionConsoleUI possessionUI)
        {
            switch (selection)
            {
                case 1:  ownerUI.SeePeople();                   break;
                case 2:  ownerUI.SeeCats();                     break;
                case 3:  ownerUI.AddOwner(OwnerTypes.Human);    break;
                case 4:  ownerUI.AddOwner(OwnerTypes.Cat);      break;
                case 5:  ownerUI.UpdateOwner(OwnerTypes.Human); break;
                case 6:  ownerUI.UpdateOwner(OwnerTypes.Cat);   break;
                case 7:  ownerUI.DeleteOwner();                 break;
                case 8:  possessionUI.SeeAllStuff();            break;
                case 9:  possessionUI.SeeAllStuffByOwner();     break;
                case 10: possessionUI.SeeAllStuffByOwnerType(); break;
                case 11: possessionUI.ChangeOwners();           break;
                case 12: possessionUI.InstallUninstallStuff();  break;
                case 13: possessionUI.UpdateStuff();            break;
                case 14: possessionUI.DeleteStuff();            break;
                case 15: possessionUI.AddStuff();               break;
                default: throw new ArgumentException();
            }
            Console.WriteLine("============================");
        }
    }
}
