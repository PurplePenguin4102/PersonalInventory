using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.Classes.Enums;
using System.Data.Entity;
using Inventory.DataModel.RulesValidation;

namespace Inventory.DataModel.Repositories
{
    public class PossessionRepository
    {
        /// <summary>
        /// insert method for a single possession
        /// </summary>
        public bool CreatePossession(Possession possession)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;

                if (possession.Owner != null)
                    possession.Owner = context.Owners.Find(possession.Owner.Id);
                if (possession.PartOf != null)
                    possession.PartOf = context.Possessions.Find(possession.PartOf.Id);
                context.Possessions.Add(possession);
                return context.SaveChanges() != 0;
            }
        }

        public bool IsTableEmpty()
        {
            using (InventoryContext Context = new InventoryContext())
            {
                return !Context.Possessions.Any();
            }
        }

        /// <summary>
        /// Inserts all the possessions given in a list
        /// </summary>
        public bool CreateLotsOfPossession(Possession[] possessions)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                foreach (Possession p in possessions)
                {
                    if (p.Owner != null)
                        p.Owner = context.Owners.Find(p.Owner.Id);
                    if (p.PartOf != null)
                        p.PartOf = context.Possessions.Find(p.PartOf.Id);
                }
                context.Possessions.AddRange(possessions);
                return context.SaveChanges() != 0;
            }
        }

        /// <summary>
        /// Updates the possession in the database
        /// </summary>
        public bool UpdatePossession(Possession updated)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                Possession inDB = context.Possessions.Find(updated.Id);
                if (inDB != null && PossessionRules.VerifyUpdate(inDB, updated))
                {
                    inDB.ApplyUpdate(updated);
                }
                return context.SaveChanges() != 0;
            }
        }

        /// <summary>
        /// Deletes a possession
        /// </summary>
        public bool DestroyPossession(Possession usrRequest)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;

                Possession toBeDeleted = context.Possessions.Find(usrRequest.Id);
                if (toBeDeleted == null)
                    return false;

                if (toBeDeleted.Equals(usrRequest))
                {
                    var possessionsInDB = context.Possessions.Where(s => s.PartOf.Id == toBeDeleted.Id).ToList();
                    foreach (var thing in possessionsInDB)
                    {
                        thing.PartOf = null;
                    }
                }
                context.SaveChanges();
                return context.SaveChanges() != 0;
            }
        }

        // queries
        /// <summary>
        /// 
        /// </summary>
        public Possession GetPossessionById(int id)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Find(id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Possession> GetAllPossessions()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Include(s => s.Owner).Include(s => s.PartOf).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Possession> GetPossessionsByCategory(PossessionCategory sc)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.Category == sc).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public List<Possession> GetPossessionsInUse()
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.InUse).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Possession> GetPossessionsInInstallation(Possession installation)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Where(s => s.PartOf.Id == installation.Id).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool GivePossessionToOwner(Possession possession, Owner owner)
        {
            if (possession.PartOf != null)
                return false;

            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                List<Possession> things = context.Possessions
                                            .Where(s => s.Id == possession.Id 
                                                     || (s.PartOf != null && s.PartOf.Id == possession.Id))
                                            .ToList();
                foreach (var thing in things)
                {
                    thing.Owner = owner;
                }

                context.SaveChanges();
            }
            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
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
                var things = context.Possessions.Where(p => p.Id == installedIn.Id || p.Id == possession.Id).ToList();
                if (things.Count != 2)
                    return false;

                if (things[0].Equals(possession))
                    things[0].PartOf = things[1];
                else
                    things[1].PartOf = things[0];
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
                var inDB = context.Possessions.Find(possession.Id);
                inDB.PartOf = null;
                return context.SaveChanges() != 0;
            }
        }

        public bool AddPossessionsToInstallation(IEnumerable<Possession> possessions, Possession installation)
        {
            foreach(var s in possessions)
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
