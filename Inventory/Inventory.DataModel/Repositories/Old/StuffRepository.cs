﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.Classes.Enums;
using System.Data.Entity;

namespace Inventory.DataModel.Repositories.Old
{
    public static class PossessionRepository
    {
        public static bool CreateStuff(Possession stuff)
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
                    context.Possessions.Attach(stuff.PartOf);
                    context.Entry(stuff.PartOf).State = EntityState.Unchanged;
                }
                context.Possessions.Add(stuff);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool IsTableEmpty()
        {
            using (InventoryContext Context = new InventoryContext())
            {
                return !Context.Possessions.Any();
            }
        }

        public static bool CreateLotsOfStuff(Possession[] stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                foreach (Possession s in stuff)
                {
                    if (s.Owner != null)
                    {
                        context.Owners.Attach(s.Owner);
                        context.Entry(s.Owner).State = EntityState.Unchanged;
                    }
                }
                context.Possessions.AddRange(stuff);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool UpdateStuff(Possession stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(stuff);
                context.Entry(stuff).State = EntityState.Modified;
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool DestroyStuff(Possession stuff)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(stuff);
                var attached = context.Possessions.Where(s => s.PartOf.Id == stuff.Id).ToList();
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
        public static Possession GetStuffById(int id)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Find(id);
            }
        }

        public static IEnumerable<Possession> GetAllStuff()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Include(s => s.Owner).Include(s => s.PartOf).ToList();
            }
        }

        public static IEnumerable<Possession> GetStuffByCategory(PossessionCategory sc)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.Category == sc).ToList();
            }
        }

        public static IEnumerable<Possession> GetStuffAfterDate(DateTime dt, bool ordered = false)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                var stuffs =  context.Possessions.Where(s => s.Acquired > dt);
                if (ordered)
                {
                    stuffs.OrderBy(s => s.Acquired);
                }
                return stuffs.ToList();
            }
        }
        public static IEnumerable<Possession> GetStuffInUse()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.InUse).ToList();
            }
        }
        public static IEnumerable<Possession> GetStuffInInstallation(Possession installation)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.PartOf.Id == installation.Id).ToList();
            }
        }

        public static bool GiveStuffToOwner(Possession stuff, Owner owner)
        {
            if (stuff.PartOf != null)
                return false;

            using (var context = new InventoryContext())
            {
                List<Possession> things = context.Possessions
                                            .Where(s => s.PartOf != null && s.PartOf.Id == stuff.Id)
                                            .ToList();

                context.Database.Log = Console.WriteLine;
                foreach (var thing in things)
                {
                    thing.Owner = owner;
                }

                context.SaveChanges();
            }
            return true;
        }
        
        public static bool UnclaimStuff(Possession stuff, Owner owner)
        {
            stuff.Owner = null;
            return UpdateStuff(stuff);
        }

        public static bool InstallStuff(Possession stuff, Possession installedIn)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(stuff);
                context.Possessions.Attach(installedIn);
                stuff.PartOf = installedIn;
                return context.SaveChanges() != 0;
            }
        }

        public static bool RemoveStuffFromInstallation(Possession stuff)
        {
            if (stuff.PartOf == null)
                return false;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(stuff);
                stuff.PartOf = null;
                return context.SaveChanges() != 0;
            }
        }

        public static bool AddStuffToInstallation(IEnumerable<Possession> stuff, Possession installation)
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
