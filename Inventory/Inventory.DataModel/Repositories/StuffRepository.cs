using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.Classes.Enums;
using System.Data.Entity;

namespace Inventory.DataModel.Repositories
{
    public static class StuffRepository
    {
        public static bool CreateStuff(Stuff stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.Add(stuff);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool CreateLotsOfStuff(Stuff[] stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.AddRange(stuff);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool UpdateStuff(Stuff stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.Attach(stuff);
                var entry = context.Entry(stuff);
                entry.State = EntityState.Modified;
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool DestroyStuff(Stuff stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.Attach(stuff);
                context.Entry(stuff).State = EntityState.Deleted;
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        // queries
        public static Stuff GetStuffById(int id)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Where(s => s.Id == id).FirstOrDefault();
            }
        }

        public static IEnumerable<Stuff> GetAllStuff()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.ToList();
            }
        }

        public static IEnumerable<Stuff> GetStuffByCategory(StuffCategory sc)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Where(s => s.Category == sc).ToList();
            }
        }

        public static IEnumerable<Stuff> GetStuffAfterDate(DateTime dt, bool ordered = false)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                var stuffs =  context.Inventory.Where(s => s.Acquired > dt);
                if (ordered)
                {
                    stuffs.OrderBy(s => s.Acquired);
                }
                return stuffs.ToList();
            }
        }
        public static IEnumerable<Stuff> GetStuffInUse()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Where(s => s.InUse).ToList();
            }
        }
        public static IEnumerable<Stuff> GetStuffInInstallation(Stuff installation)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Where(s => s.PartOf.Id == installation.Id).ToList();
            }
        }

        public static bool GiveStuffToOwner(Stuff stuff, Owner owner)
        {
            stuff.Owner = owner;
            return UpdateStuff(stuff);
        }

        public static bool UnclaimStuff(Stuff stuff, Owner owner)
        {
            stuff.Owner = null;
            return UpdateStuff(stuff);
        }

        public static bool InstallStuff(Stuff stuff, Stuff installedIn)
        {
            stuff.PartOf = installedIn;
            return UpdateStuff(stuff);
        }

        public static bool RemoveStuffFromInstallation(Stuff stuff, Stuff installation)
        {
            stuff.PartOf = null;
            return UpdateStuff(stuff);
        }

        public static bool AddStuffToInstallation(IEnumerable<Stuff> stuff, Stuff installation)
        {
            foreach(var s in stuff)
            {
                bool success = InstallStuff(s, installation);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
