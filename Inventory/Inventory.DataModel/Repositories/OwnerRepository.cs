using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.PCL.Classes;
using Inventory.PCL.Classes.Enums;
using System.Data.Entity;
using Inventory.DataModel.RulesValidation;

namespace Inventory.DataModel.Repositories
{
    public class OwnerRepository : IDisposable
    {
        private InventoryContext _DB;

        public OwnerRepository(InventoryContext DB)
        {
            _DB = DB;
            //_DB.Database.Log = Console.WriteLine;
        }

        /// <summary>
        /// Inserts an Owner into the database
        /// </summary>
        public bool CreateOwner(Owner newOwner)
        {
            if (OwnerRules.VerifyInsert(newOwner))
            {
                _DB.Owners.Add(newOwner);
                return _DB.SaveChanges() != 0;
            }
            else
                return false;
        }

        /// <summary>
        /// Create multiple owners
        /// </summary>
        public bool CreateOwners(IEnumerable<Owner> newOwners)
        {
            if (newOwners.All(o => OwnerRules.VerifyInsert(o)))
            {
                _DB.Owners.AddRange(newOwners);
                return _DB.SaveChanges() != 0;
            }
            else
                return false;
        }

        /// <summary>
        /// Fetches the owner by the database ID 
        /// </summary>
        public Owner GetOwnerById(int id)
        {
            return _DB.Owners.Find(id);
        }

        /// <summary>
        /// Fetches all owners in the DB
        /// </summary>
        public List<Owner> GetAllOwners()
        {
            return _DB.Owners.ToList();
        }

        /// <summary>
        /// gets all owners with the same first name
        /// </summary>
        public List<Owner> GetOwnersByFirstName(string Name)
        {
            return 
                (from owner in _DB.Owners
                 where owner.FirstName == Name
                 select owner).ToList();
        }

        /// <summary>
        /// gets all owners with the same last name
        /// </summary>
        public List<Owner> GetOwnersByLastName(string Name)
        {
            return _DB.Owners.Where(owner => owner.LastName.ToLower() == Name.ToLower()).ToList();
        }

        /// <summary>
        /// gets all owners with the same type
        /// </summary>
        public List<Owner> GetOwnersByType(OwnerTypes type)
        {
            return (from owner in _DB.Owners
                    where owner.Type == type
                    select owner).ToList();
        }

        /// <summary>
        /// Get all owners of the same gender
        /// </summary>
        public List<Owner> GetOwnersByGender(Gender gender)
        {
            return _DB.Owners.Where(owner => owner.Gender == gender).ToList();
        }

        /// <summary>
        /// Get all possessions that an owner owns
        /// </summary>
        public List<Possession> GetOwnersPossessions(Owner owner)
        {
            return _DB.Possessions.Include(s => s.Owner).Where(s => s.Owner.Id == owner.Id).ToList();
        }

        /// <summary>
        /// Get all the stuff owned by the cats / by the people
        /// </summary>
        public List<Possession> GetPossessionsByOwnerType(OwnerTypes type)
        {
            return
                (from possessions in _DB.Possessions
                 where possessions.Owner.Type == type
                 select possessions).ToList();
        }

        /// <summary>
        /// Updates the owner from user provided data
        /// </summary>
        public bool UpdateOwner(Owner updated)
        {
            if (OwnerRules.VerifyUpdate(updated))
                return _DB.SaveChanges() != 0;
            else
                return false;
        }

        /// <summary>
        /// Destroys the owner according to user provided data
        /// </summary>
        public bool DestroyOwner(Owner owner)
        {
            var stuffOwned = _DB.Possessions.Where(s => s.Owner.Id == owner.Id).ToList();
            foreach(var s in stuffOwned)
            {
                s.Owner = null;
            }
            _DB.SaveChanges();
            _DB.Entry(owner).State = EntityState.Deleted;
            return _DB.SaveChanges() != 0;
        }

        public bool IsTableEmpty()
        {
            return !_DB.Owners.Any();
        }

        public void Dispose()
        {
            _DB.Dispose();
        }
    }
}
