using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataModel.Repositories;
using Inventory.Classes;
using Inventory.ConsoleUI.Util;
using Inventory.Classes.Enums;

namespace Inventory.ConsoleUI
{
    public static class PossessionConsoleUI
    {
        public static void SeeAllStuff()
        {
            Possessions possessions = PossessionRepository.GetAllPossessions().ToList();
            Console.WriteLine(possessions.ToString());
        }

        public static void SeeAllStuffByOwner()
        {
            List<Owner> owners = OwnerRepository.GetAllOwners().ToList();
            List<Option> options = Option.OptionsFromOwners(owners);
            Console.WriteLine(owners.ToString());
            Owner owner = TextParser.SelectItemFromList("Please select an owner : ", options).Data as Owner;
            List<Possession> possessions = OwnerRepository.GetOwnersStuff(owner).ToList();
            Console.WriteLine(possessions.ToString());
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

            List<Possession> possessions = OwnerRepository.GetOwnersStuffByType(type).ToList();
            Console.WriteLine(possessions.ToString());

        }

        public static void ChangeOwners()
        {
            List<Possession> possessions = PossessionRepository.GetAllPossessions().ToList();
            List<Owner> owners = OwnerRepository.GetAllOwners().ToList(); 
            Console.WriteLine(possessions.ToString());

            Possession thing = TextParser.SelectItemFromList("Please select a possession : ", Option.OptionsFromPossessions(possessions)).Data as Possession;
            Owner owner = TextParser.SelectItemFromList("Please select a new owner : ", Option.OptionsFromOwners(owners)).Data as Owner;


            if (PossessionRepository.GivePossessionToOwner(thing, owner))
                Console.WriteLine(thing);
            else
                Console.WriteLine("I can't do that, it belongs to an installation");
            
        }

        public static void InstallUninstallStuff()
        {
            List<Possession> possessions = PossessionRepository.GetAllPossessions().ToList();
            Console.WriteLine(possessions.ToString());
            Possession thing1 = TextParser.SelectItemFromList("Please select an object to install : ", Option.OptionsFromPossessions(possessions)).Data as Possession;
            if (thing1.PartOf != null)
            {
                Console.Write($"Object is installed in {thing1.PartOf.Name}... would you like to uninstall it? (y/n) ");
                string yn = Console.ReadLine();
                if (yn == "y")
                {
                    PossessionRepository.RemovePossessionFromInstallation(thing1);
                }
                Console.Write("Would you like to install it into something else? (y/n) ");
                yn = Console.ReadLine();
                if (yn == "n")
                {
                    return;
                }
            }
            Possession thing2 = TextParser.SelectItemFromList($"Please select another object to install {thing1.Name} into : ", Option.OptionsFromPossessions(possessions)).Data as Possession;

            PossessionRepository.InstallPossession(thing1, thing2);
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
