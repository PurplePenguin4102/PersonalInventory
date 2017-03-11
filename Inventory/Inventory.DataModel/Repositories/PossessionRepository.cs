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
    public class PossessionRepository
    {
        public bool CreatePossession(Possession possession)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                if (possession.Owner != null)
                {
                    context.Owners.Attach(possession.Owner);
                    context.Entry(possession.Owner).State = EntityState.Unchanged;
                }
                if (possession.PartOf != null)
                {
                    context.Possessions.Attach(possession.PartOf);
                    context.Entry(possession.PartOf).State = EntityState.Unchanged;
                }
                context.Possessions.Add(possession);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public bool IsTableEmpty()
        {
            using (InventoryContext Context = new InventoryContext())
            {
                return !Context.Possessions.Any();
            }
        }

        public bool CreateLotsOfPossession(Possession[] possession)
        {
            int retVal;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                foreach (Possession p in possession)
                {
                    if (p.Owner != null)
                    {
                        context.Owners.Attach(p.Owner);
                        context.Entry(p.Owner).State = EntityState.Unchanged;
                    }
                }
                context.Possessions.AddRange(possession);
                retVal = context.SaveChanges();
            }
            return retVal != 0;
        }

        public bool UpdatePossession(Possession possession)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(possession);
                context.Entry(possession).State = EntityState.Modified;
                return context.SaveChanges() != 0;
            }
        }

        public bool DestroyPossession(Possession possession)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(possession);
                var attached = context.Possessions.Where(s => s.PartOf.Id == possession.Id).ToList();
                foreach (var thing in attached)
                {
                    thing.PartOf = null;
                }
                context.SaveChanges();
                context.Entry(possession).State = EntityState.Deleted;
                return context.SaveChanges() != 0;
            }
        }

        // queries
        public Possession GetPossessionById(int id)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Find(id);
            }
        }

        public List<Possession> GetAllPossessions()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Include(s => s.Owner).Include(s => s.PartOf).ToList();
            }
        }

        public List<Possession> GetPossessionsByCategory(PossessionCategory sc)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.Category == sc).ToList();
            }
        }

        public List<Possession> GetPossessionsAfterDate(DateTime dt, bool ordered = false)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                var Possessions =  context.Possessions.Where(s => s.Acquired > dt);
                if (ordered)
                {
                    Possessions.OrderBy(s => s.Acquired);
                }
                return Possessions.ToList();
            }
        }

        public List<Possession> GetPossessionsInUse()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.InUse).ToList();
            }
        }

        public List<Possession> GetPossessionsInInstallation(Possession installation)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.PartOf.Id == installation.Id).ToList();
            }
        }

        public bool GivePossessionToOwner(Possession possession, Owner owner)
        {
            if (possession.PartOf != null)
                return false;

            using (var context = new InventoryContext())
            {
                List<Possession> things = context.Possessions
                                            .Where(s => s.PartOf != null && s.PartOf.Id == possession.Id)
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
        
        public bool UnclaimPossession(Possession possession, Owner owner)
        {
            possession.Owner = null;
            return UpdatePossession(possession);
        }

        public bool InstallPossession(Possession possession, Possession installedIn)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(possession);
                context.Possessions.Attach(installedIn);
                possession.PartOf = installedIn;
                return context.SaveChanges() != 0;
            }
        }

        public bool RemovePossessionFromInstallation(Possession possession)
        {
            if (possession.PartOf == null)
                return false;
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Possessions.Attach(possession);
                possession.PartOf = null;
                return context.SaveChanges() != 0;
            }
        }

        public bool AddPossessionToInstallation(IEnumerable<Possession> possession, Possession installation)
        {
            foreach(var s in possession)
            {
                bool success = InstallPossession(s, installation);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
