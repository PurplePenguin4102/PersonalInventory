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
        public static void SeedOwners()
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

        public static void SeedStuff()
        {
            Owner Joey = OwnerRepository.GetOwnerByFirstName("Joey").FirstOrDefault();
            Owner Kate = OwnerRepository.GetOwnerByFirstName("Kate").FirstOrDefault();
            Owner Mozart = OwnerRepository.GetOwnerByLastName("Mozart").FirstOrDefault();
            Owner Phryne = OwnerRepository.GetOwnerByFirstName("Phryne").FirstOrDefault();

            Stuff TV = new Stuff
            {
                Name = "'4K' Hisense TV ",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = Joey,
                InUse = true,
            };
            Stuff Cable1 = new Stuff
            {
                Name = "HDMI Cable 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Cable",
                Owner = Joey,
                InUse = true,
                PartOf = TV
            };
            Stuff Cable2 = new Stuff
            {
                Name = "AC Power 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Cable",
                Owner = Joey,
                InUse = true,
                PartOf = TV
            };
            Stuff Mousey = new Stuff
            {
                Name = "Mousey",
                Acquired = new DateTime(2015, 5, 3),
                Category = StuffCategory.Pet,
                SubCategory = "Toy",
                Owner = Phryne,
                InUse = false,
            };
            Stuff Castle = new Stuff
            {
                Name = "Cat Castle",
                Acquired = new DateTime(2016, 4, 12),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = Phryne,
                InUse = true,
            };
            Stuff Fountain = new Stuff
            {
                Name = "Water Fountain",
                Acquired = new DateTime(2015, 8, 11),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = Mozart,
                InUse = true,
            };
            Stuff FishingLine = new Stuff
            {
                Name = "Cat Fishing Teaser",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Pet,
                SubCategory = "Toy",
                Owner = Phryne,
                InUse = false,
            };
            Stuff JoeysChair = new Stuff
            {
                Name = "Joey's Chair",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = Mozart,
                InUse = true,
            };
            Stuff Tablet = new Stuff
            {
                Name = "Samsung Galaxy Tab A",
                Acquired = new DateTime(2015, 8, 20),
                Category = StuffCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = Kate,
                InUse = true,
            };
            Stuff FishTank = new Stuff
            {
                Name = "Fish Tank",
                Acquired = new DateTime(2008, 1, 5),
                Category = StuffCategory.Entertainment,
                SubCategory = "Pet",
                Owner = Kate,
                InUse = false,
            };
            Stuff Shoes = new Stuff
            {
                Name = "Shoes",
                Acquired = new DateTime(2016, 1, 23),
                Category = StuffCategory.Clothing,
                Owner = Kate,
                InUse = false,
            };
            StuffRepository.CreateLotsOfStuff(new Stuff[] { Shoes, FishTank, Tablet, JoeysChair, FishingLine, Fountain, Castle, Mousey, Cable1, Cable2, TV });
        }
    }
}
