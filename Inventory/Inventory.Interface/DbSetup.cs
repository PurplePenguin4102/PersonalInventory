using Inventory.Classes;
using Inventory.Classes.Enums;
using Inventory.DataModel;
using Inventory.DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Interface
{
    public static class DbSetup
    {
        public static void SetUpDB()
        {
            Owner Joey = new Owner
            {
                FirstName = "Joey",
                LastName = "Ray",
                Birthday = new DateTime(1987, 7, 20),
                Gender = Gender.Male,
                Type = OwnerTypes.Human,
            };
            Owner Kate = new Owner
            {
                FirstName = "Kate",
                LastName = "Tillack",
                Birthday = new DateTime(1993, 5, 31),
                Gender = Gender.Female,
                Type = OwnerTypes.Human,
            };
            Owner Mozart = new Owner
            {
                FirstName = "WolfCat",
                LastName = "Mozart",
                Birthday = new DateTime(2015, 3, 25),
                Gender = Gender.Male,
                Type = OwnerTypes.Cat,
            };
            Owner Phryne = new Owner
            {
                FirstName = "Phryne",
                LastName = "Fisheater",
                Birthday = new DateTime(2015, 3, 25),
                Gender = Gender.Female,
                Type = OwnerTypes.Cat,
            };

            Owner[] owners = new Owner[4] { Joey, Kate, Mozart, Phryne };

            OwnerRepository.CreateOwner(owners);
        }
    }
}
