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
            throw new NotImplementedException();
        }

        public static void SeeAllStuffByOwnerType()
        {
            throw new NotImplementedException();
        }

        public static void ChangeOwners()
        {
            throw new NotImplementedException();
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
