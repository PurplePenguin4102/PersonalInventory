using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataModel.Repositories;
using Inventory.Classes;
using Inventory.ConsoleUI.Util;
using Inventory.ConsoleUI.FunctionExtensions;
using Inventory.Classes.Enums;

namespace Inventory.ConsoleUI
{
    public static class StuffConsoleUI
    {
        public static void SeeAllStuff()
        {
            List<Stuff> stuffs = StuffRepository.GetAllStuff().ToList();
            Console.WriteLine(stuffs.ToString(new object()));
        }

        public static void SeeAllStuffByOwner()
        {
            List<Owner> owners = OwnerRepository.GetAllOwners().ToList();
            List<Option> options = Option.OptionsFromOwners(owners);
            Console.WriteLine(owners.ToString(new object()));
            Owner owner = TextParser.SelectItemFromList("Please select an owner : ", options).Data as Owner;
            List<Stuff> stuffs = OwnerRepository.GetOwnersStuff(owner).ToList();
            Console.WriteLine(stuffs.ToString(new object()));
        }

        public static void SeeAllStuffByOwnerType()
        {
            int ans;
            ans = TextParser.MakeSelection(typeof(OwnerTypes));
            OwnerTypes type;
            if (Enum.IsDefined(typeof(OwnerTypes), ans))
            {
                type = (OwnerTypes)ans;
            }
            else return;

            List<Stuff> stuffs = OwnerRepository.GetOwnersStuffByType(type).ToList();
            Console.WriteLine(stuffs.ToString(new object()));

        }

        public static void ChangeOwners()
        {
            List<Stuff> stuffs = StuffRepository.GetAllStuff().ToList();
            List<Owner> owners = OwnerRepository.GetAllOwners().ToList(); 
            Console.WriteLine(stuffs.ToString(new object()));

            Stuff thing = TextParser.SelectItemFromList("Please select a possession : ", Option.OptionsFromStuffs(stuffs)).Data as Stuff;
            Owner owner = TextParser.SelectItemFromList("Please select a new owner : ", Option.OptionsFromOwners(owners)).Data as Owner;


            if (StuffRepository.GiveStuffToOwner(thing, owner))
                Console.WriteLine(thing);
            else
                Console.WriteLine("I can't do that, it belongs to an installation");
            
        }

        public static void InstallUninstallStuff()
        {
            List<Stuff> stuffs = StuffRepository.GetAllStuff().ToList();
            Console.WriteLine(stuffs.ToString(new object()));
            Stuff thing1 = TextParser.SelectItemFromList("Please select an object to install : ", Option.OptionsFromStuffs(stuffs)).Data as Stuff;
            if (thing1.PartOf != null)
            {
                Console.Write($"Object is installed in {thing1.PartOf.Name}... would you like to uninstall it? (y/n) ");
                string yn = Console.ReadLine();
                if (yn == "y")
                {
                    StuffRepository.RemoveStuffFromInstallation(thing1);
                }
                Console.Write("Would you like to install it into something else? (y/n) ");
                yn = Console.ReadLine();
                if (yn == "n")
                {
                    return;
                }
            }
            Stuff thing2 = TextParser.SelectItemFromList($"Please select another object to install {thing1.Name} into : ", Option.OptionsFromStuffs(stuffs)).Data as Stuff;

            StuffRepository.InstallStuff(thing1, thing2);
            Console.WriteLine($"Updated {thing1.Name} to be part of {thing2.Name}");
            Console.WriteLine(thing1);
            Console.WriteLine(thing2);
            }

        public static void UpdateStuff()
        {
            throw new NotImplementedException();
        }

        public static void DeleteStuff()
        {
            throw new NotImplementedException();
        }

        public static void AddStuff()
        {
            throw new NotImplementedException();
        }
    }
}
