using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataModel.Repositories;
using Inventory.Classes;
using Inventory.Interface.Util;
using Inventory.Interface.FunctionExtensions;
using Inventory.Classes.Enums;

namespace Inventory.Interface
{
    public static class StuffInterface
    {
        public static void SeeAllStuff()
        {
            List<Stuff> stuffs = StuffRepository.GetAllStuff().ToList();
            Console.WriteLine(stuffs.ToString(new object()));
        }

        public static void SeeAllStuffByOwner()
        {
            List<Owner> owners = OwnerRepository.GetAllOwners().ToList();
            Console.WriteLine(owners.ToString(new object()));
            Owner owner;
            if (TextParser.MakeSelection(out owner, "Please select an owner : ", owners))
            {
                List<Stuff> stuffs = OwnerRepository.GetOwnersStuff(owner).ToList();
                Console.WriteLine(stuffs.ToString(new object()));
            }
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
            Stuff thing;

            Owner owner;
            if (TextParser.MakeSelection(out thing, "Please select an object : ", stuffs) &&
                TextParser.MakeSelection(out owner, "Please select an owner : ", owners))
            {
                if (StuffRepository.GiveStuffToOwner(thing, owner))
                    Console.WriteLine(thing);
                else
                    Console.WriteLine("I can't do that, it belongs to an installation");
            }
        }

        public static void InstallUninstallStuff()
        {
            throw new NotImplementedException();
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
