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
    public class PossessionRepository : IDisposable
    {

        InventoryContext _DB;

        public PossessionRepository(InventoryContext DB)
        {
            _DB = DB;
            _DB.Database.Log = Console.WriteLine;
        }

        /// <summary>
        /// insert method for a single possession
        /// </summary>
        public bool CreatePossession(Possession possession)
        {
            if (possession.Owner != null)
                possession.Owner = _DB.Owners.Find(possession.Owner.Id);
            if (possession.PartOf != null)
                possession.PartOf = _DB.Possessions.Find(possession.PartOf.Id);
            _DB.Possessions.Add(possession);
            return _DB.SaveChanges() != 0;
        }

        public bool IsTableEmpty()
        {
            return !_DB.Possessions.Any();
        }

        /// <summary>
        /// Inserts all the possessions given in a list
        /// </summary>
        public bool CreateLotsOfPossession(Possession[] possessions)
        {
            foreach (Possession p in possessions)
            {
                if (p.Owner != null)
                    p.Owner = _DB.Owners.Find(p.Owner.Id);
                if (p.PartOf != null)
                    p.PartOf = _DB.Possessions.Find(p.PartOf.Id);
            }
            _DB.Possessions.AddRange(possessions);
            return _DB.SaveChanges() != 0;
        }

        /// <summary>
        /// Updates the possession in the database
        /// </summary>
        public bool UpdatePossession(Possession updated)
        {
            Possession inDB = _DB.Possessions.Find(updated.Id);
            if (inDB != null && PossessionRules.VerifyUpdate(inDB, updated))
            {
                inDB.ApplyUpdate(updated);
            }
            return _DB.SaveChanges() != 0;
        }

        /// <summary>
        /// Deletes a possession
        /// </summary>
        public bool DestroyPossession(Possession usrRequest)
        {
            Possession toBeDeleted = _DB.Possessions.Find(usrRequest.Id);
            if (toBeDeleted == null)
                return false;

            if (toBeDeleted.Equals(usrRequest))
            {
                var possessionsInDB = _DB.Possessions.Where(s => s.PartOf.Id == toBeDeleted.Id).ToList();
                foreach (var thing in possessionsInDB)
                {
                    thing.PartOf = null;
                }
            }
            _DB.SaveChanges();
            return _DB.SaveChanges() != 0;
        }

        // queries
        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public Possession GetPossessionById(int id)
        {
            return _DB.Possessions.Find(id);
        }

        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public List<Possession> GetAllPossessions()
        {
            return _DB.Possessions.Include(s => s.Owner).Include(s => s.PartOf).ToList();
        }

        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public List<Possession> GetPossessionsByCategory(PossessionCategory sc)
        {
            return _DB.Possessions.Where(s => s.Category == sc).ToList();
        }

        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public List<Possession> GetPossessionsAfterDate(DateTime dt, bool ordered = false)
        {
            var Possessions =  _DB.Possessions.Where(s => s.Acquired > dt);
            if (ordered)
            {
                Possessions.OrderBy(s => s.Acquired);
            }
            return Possessions.ToList();
        }

        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public List<Possession> GetPossessionsInUse()
        {
            return _DB.Possessions.Where(s => s.InUse).ToList();
        }

        /// <summary>
        /// Does what it says on the box
        /// </summary>
        public List<Possession> GetPossessionsInInstallation(Possession installation)
        {
            return _DB.Possessions.Where(s => s.PartOf.Id == installation.Id).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool GivePossessionToOwner(Possession possession, Owner owner)
        {
            if (possession.PartOf != null)
                return false;

            List<Possession> things = _DB.Possessions
                                        .Where(s => s.Id == possession.Id 
                                                    || (s.PartOf != null && s.PartOf.Id == possession.Id))
                                        .ToList();
            foreach (var thing in things)
            {
                thing.Owner = owner;
            }

            _DB.SaveChanges();
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
            var things = _DB.Possessions.Where(p => p.Id == installedIn.Id || p.Id == possession.Id).ToList();
            if (things.Count != 2)
                return false;

            if (things[0].Equals(possession))
                things[0].PartOf = things[1];
            else
                things[1].PartOf = things[0];
            return _DB.SaveChanges() != 0;
        }

        public bool RemovePossessionFromInstallation(Possession possession)
        {
            if (possession.PartOf == null)
                return false;
            _DB.Database.Log = Console.WriteLine;
            var inDB = _DB.Possessions.Find(possession.Id);
            inDB.PartOf = null;
            return _DB.SaveChanges() != 0;
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

        public void Dispose()
        {
            _DB.Dispose();
        }
    }
}
