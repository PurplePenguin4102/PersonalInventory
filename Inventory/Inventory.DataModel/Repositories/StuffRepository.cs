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
                if (stuff.Owner != null)
                {
                    context.Owners.Attach(stuff.Owner);
                    context.Entry(stuff.Owner).State = EntityState.Unchanged;
                }
                if (stuff.PartOf != null)
                {
                    context.Inventory.Attach(stuff.PartOf);
                    context.Entry(stuff.PartOf).State = EntityState.Unchanged;
                }
                context.Inventory.Add(stuff);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool IsTableEmpty()
        {
            using (InventoryContext Context = new InventoryContext())
            {
                return !Context.Inventory.Any();
            }
        }

        public static bool CreateLotsOfStuff(Stuff[] stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                foreach (Stuff s in stuff)
                {
                    if (s.Owner != null)
                    {
                        context.Owners.Attach(s.Owner);
                        context.Entry(s.Owner).State = EntityState.Unchanged;
                    }
                }
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
                context.Entry(stuff).State = EntityState.Modified;
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
                var attached = context.Inventory.Where(s => s.PartOf.Id == stuff.Id).ToList();
                foreach (var thing in attached)
                {
                    thing.PartOf = null;
                }
                context.SaveChanges();
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
                return context.Inventory.Find(id);
            }
        }

        public static IEnumerable<Stuff> GetAllStuff()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Include(s => s.Owner).Include(s => s.PartOf).ToList();
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
            if (stuff.PartOf != null)
                return false;
            List<Stuff> things = new List<Stuff>();
            GetAllSubStuff(stuff, things);
            int count = 0;
            foreach (var thing in things)
            {
                using (var context = new InventoryContext())
                {
                    context.Database.Log = Console.WriteLine;
                    context.Owners.Attach(owner);
                    context.Inventory.Attach(thing);
                    thing.Owner = owner;
                    count += context.SaveChanges();
                }
            }
            return count != 0;
        }

        private static void GetAllSubStuff(Stuff thing, List<Stuff> things)
        {
            things.Add(thing);
            List<Stuff> stuffs = GetAllStuff().Where(s => s.PartOf != null && s.PartOf.Id == thing.Id).ToList();
            foreach(var item in stuffs)
            {
                GetAllSubStuff(item, things);
            }
        }

        public static bool UnclaimStuff(Stuff stuff, Owner owner)
        {
            stuff.Owner = null;
            return UpdateStuff(stuff);
        }

        public static bool InstallStuff(Stuff stuff, Stuff installedIn)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.Attach(stuff);
                context.Inventory.Attach(installedIn);
                stuff.PartOf = installedIn;
                return context.SaveChanges() != 0;
            }
        }

        public static bool RemoveStuffFromInstallation(Stuff stuff)
        {
            if (stuff.PartOf == null)
                return false;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Inventory.Attach(stuff);
                stuff.PartOf = null;
                return context.SaveChanges() != 0;
            }
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
