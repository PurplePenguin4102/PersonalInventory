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
    public class PossessionConsoleUI
    {
        private readonly PossessionRepository possession_DB;
        private readonly OwnerRepository owner_DB;

        public PossessionConsoleUI(PossessionRepository possessions, OwnerRepository owners)
        {
            possession_DB = possessions;
            owner_DB = owners;
        }

        public void SeeAllStuff()
        {
            Possessions possessions = possession_DB.GetAllPossessions();
            Console.WriteLine(possessions.ToString());
        }

        public void SeeAllStuffByOwner()
        {
            Owners owners = owner_DB.GetAllOwners();
            Console.WriteLine(owners.ToString());
            Owner owner = TextParser.SelectItemFromList<Owner>("Please select an owner : ", owners);
            Possessions possessions = owner_DB.GetOwnersPossessions(owner).ToList();
            Console.WriteLine(possessions.ToString());
        }

        public void SeeAllStuffByOwnerType()
        {
            int ans;
            ans = TextParser.MakeSelection(typeof(OwnerTypes));
            OwnerTypes type;
            if (Enum.IsDefined(typeof(OwnerTypes), ans))
            {
                type = (OwnerTypes)ans;
            }
            else return;

            Possessions possessions = owner_DB.GetPossessionsByOwnerType(type).ToList();
            Console.WriteLine(possessions.ToString());

        }

        public void ChangeOwners()
        {
            Possessions possessions = possession_DB.GetAllPossessions().ToList();
            Owners owners = owner_DB.GetAllOwners().ToList(); 
            Console.WriteLine(possessions.ToString());

            Possession thing = TextParser.SelectItemFromList<Possession>("Please select a possession : ", possessions);
            Owner owner = TextParser.SelectItemFromList<Owner>("Please select a new owner : ", owners);


            if (possession_DB.GivePossessionToOwner(thing, owner))
                Console.WriteLine(thing);
            else
                Console.WriteLine("I can't do that, it belongs to an installation");
            
        }

        public void InstallUninstallStuff()
        {
            Possessions possessions = possession_DB.GetAllPossessions().ToList();
            Console.WriteLine(possessions.ToString());
            Possession thing1 = TextParser.SelectItemFromList<Possession>("Please select an object to install : ", possessions);
            if (thing1.PartOf != null)
            {
                Console.Write($"Object is installed in {thing1.PartOf.Name}... would you like to uninstall it? (y/n) ");
                string yn = Console.ReadLine();
                if (yn == "y")
                {
                    possession_DB.RemovePossessionFromInstallation(thing1);
                }
                Console.Write("Would you like to install it into something else? (y/n) ");
                yn = Console.ReadLine();
                if (yn == "n")
                {
                    return;
                }
            }
            Possession thing2 = TextParser.SelectItemFromList<Possession>($"Please select another object to install {thing1.Name} into : ", possessions);

            possession_DB.InstallPossession(thing1, thing2);
            Console.WriteLine($"Updated {thing1.Name} to be part of {thing2.Name}");
            Console.WriteLine(thing1);
            Console.WriteLine(thing2);
            }

        public void UpdateStuff()
        {
            throw new NotImplementedException();
        }

        public void DeleteStuff()
        {
            throw new NotImplementedException();
        }

        public void AddStuff()
        {
            throw new NotImplementedException();
        }
    }
}
