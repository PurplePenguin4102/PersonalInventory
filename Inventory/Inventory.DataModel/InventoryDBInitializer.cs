using Inventory.Classes;
using Inventory.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataModel
{
    class InventoryDBInitializer : DropCreateDatabaseAlways<InventoryContext>
    {
        protected override void Seed(InventoryContext context)
        {
            context.Owners.Add(new Owner
            {
                FirstName = "Joey",
                LastName = "Ray",
                Birthday = new DateTime(1987, 7, 20),
                Gender = Gender.Male,
                Type = OwnerTypes.Human,
            });
            context.Owners.Add(new Owner
            {
                FirstName = "Kate",
                LastName = "Tillack",
                Birthday = new DateTime(1993, 5, 31),
                Gender = Gender.Female,
                Type = OwnerTypes.Human,
            });
            context.Owners.Add(new Owner
            {
                FirstName = "WolfCat",
                LastName = "Mozart",
                Birthday = new DateTime(2015, 3, 25),
                Gender = Gender.Male,
                Type = OwnerTypes.Cat,
            });
            context.Owners.Add(new Owner
            {
                FirstName = "Phryne",
                LastName = "Fisheater",
                Birthday = new DateTime(2015, 3, 25),
                Gender = Gender.Female,
                Type = OwnerTypes.Cat,
            });

            context.Inventory.Add(new Stuff
            {
                Name = "Shoes",
                Acquired = new DateTime(2016, 1, 23),
                Category = StuffCategory.Clothing,
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = false,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Fish Tank",
                Acquired = new DateTime(2008, 1, 5),
                Category = StuffCategory.Entertainment,
                SubCategory = "Pet",
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = false,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Samsung Galaxy Tab A",
                Acquired = new DateTime(2015, 8, 20),
                Category = StuffCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = true,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Joey's Chair",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "WolfCat").Single(),
                InUse = true,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Cat Fishing Teaser",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Pet,
                SubCategory = "Toy",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = false,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Water Fountain",
                Acquired = new DateTime(2015, 8, 11),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "WolfCat").Single(),
                InUse = true,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Cat Castle",
                Acquired = new DateTime(2016, 4, 12),
                Category = StuffCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = true,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "Mousey",
                Acquired = new DateTime(2015, 5, 3),
                Category = StuffCategory.Pet,
                SubCategory = "Toy",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = false,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "'4K' Hisense TV ",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
            });
            context.Inventory.Add(new Stuff
            {
                Name = "AC Power 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Cable",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
                PartOf = context.Inventory.Where(o => o.Acquired == new DateTime(2010, 7, 23)).Single(),
            });
            context.Inventory.Add(new Stuff
            {
                Name = "HDMI Cable 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = StuffCategory.Electronics,
                SubCategory = "Cable",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
                PartOf = context.Inventory.Where(o => o.Acquired == new DateTime(2010, 7, 23)).Single(),
            });

            base.Seed(context);
        }
    }
}
