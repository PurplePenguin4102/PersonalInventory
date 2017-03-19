using Inventory.PCL.Classes;
using Inventory.PCL.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataModel
{
    class InventoryDBInitializer : DropCreateDatabaseIfModelChanges<InventoryContext>
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

            context.Possessions.Add(new Possession
            {
                Name = "Shoes",
                Acquired = new DateTime(2016, 1, 23),
                Category = PossessionCategory.Clothing,
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = false,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Fish Tank",
                Acquired = new DateTime(2008, 1, 5),
                Category = PossessionCategory.Entertainment,
                SubCategory = "Pet",
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = false,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Samsung Galaxy Tab A",
                Acquired = new DateTime(2015, 8, 20),
                Category = PossessionCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = context.Owners.Where(o => o.FirstName == "Kate").Single(),
                InUse = true,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Joey's Chair",
                Acquired = new DateTime(2010, 7, 23),
                Category = PossessionCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "WolfCat").Single(),
                InUse = true,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Cat Fishing Teaser",
                Acquired = new DateTime(2010, 7, 23),
                Category = PossessionCategory.Pet,
                SubCategory = "Toy",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = false,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Water Fountain",
                Acquired = new DateTime(2015, 8, 11),
                Category = PossessionCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "WolfCat").Single(),
                InUse = true,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Cat Castle",
                Acquired = new DateTime(2016, 4, 12),
                Category = PossessionCategory.Pet,
                SubCategory = "Furniture",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = true,
            });
            context.Possessions.Add(new Possession
            {
                Name = "Mousey",
                Acquired = new DateTime(2015, 5, 3),
                Category = PossessionCategory.Pet,
                SubCategory = "Toy",
                Owner = context.Owners.Where(o => o.FirstName == "Phryne").Single(),
                InUse = false,
            });
            context.Possessions.Add(new Possession
            {
                Name = "'4K' Hisense TV ",
                Acquired = new DateTime(2010, 7, 23),
                Category = PossessionCategory.Electronics,
                SubCategory = "Entertainment",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
            });
            context.Possessions.Add(new Possession
            {
                Name = "AC Power 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = PossessionCategory.Electronics,
                SubCategory = "Cable",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
                PartOf = context.Possessions.Where(o => o.Acquired == new DateTime(2010, 7, 23)).Single(),
            });
            context.Possessions.Add(new Possession
            {
                Name = "HDMI Cable 5\"",
                Acquired = new DateTime(2010, 7, 23),
                Category = PossessionCategory.Electronics,
                SubCategory = "Cable",
                Owner = context.Owners.Where(o => o.FirstName == "Joey").Single(),
                InUse = true,
                PartOf = context.Possessions.Where(o => o.Acquired == new DateTime(2010, 7, 23)).Single(),
            });

            base.Seed(context);
        }
    }
}
