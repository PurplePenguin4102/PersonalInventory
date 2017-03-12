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
    public class OwnerRepository
    {
        /// <summary>
        /// Inserts an Owner into the database
        /// </summary>
        public bool CreateOwner(Owner newOwner)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                if (OwnerRules.VerifyInsert(newOwner))
                    Context.Owners.Add(newOwner);
                return Context.SaveChanges() != 0;
            }
        }

        /// <summary>
        /// Create multiple owners
        /// </summary>
        public bool CreateOwners(IEnumerable<Owner> newOwners)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                if (newOwners.All(o => OwnerRules.VerifyInsert(o)))
                    Context.Owners.AddRange(newOwners);
                return Context.SaveChanges() != 0;
            }
        }

        /// <summary>
        /// Fetches the owner by the database ID 
        /// </summary>
        public Owner GetOwnerById(int id)
        {
            using (var Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return Context.Owners.Find(id);
            }
        }

        /// <summary>
        /// Fetches all owners in the DB
        /// </summary>
        public List<Owner> GetAllOwners()
        {
            using (var Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return Context.Owners.ToList();
            }
        }

        /// <summary>
        /// gets all owners with the same first name
        /// </summary>
        public List<Owner> GetOwnersByFirstName(string Name)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return 
                    (from owner in Context.Owners
                     where owner.FirstName == Name
                     select owner).ToList();
            }
        }

        /// <summary>
        /// gets all owners with the same last name
        /// </summary>
        public List<Owner> GetOwnersByLastName(string Name)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return Context.Owners.Where(owner => owner.LastName.ToLower() == Name.ToLower()).ToList();
            }
        }

        /// <summary>
        /// gets all owners with the same type
        /// </summary>
        public List<Owner> GetOwnersByType(OwnerTypes type)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return (from owner in Context.Owners
                        where owner.Type == type
                        select owner).ToList();
            }
        }

        /// <summary>
        /// Get all owners of the same gender
        /// </summary>
        public List<Owner> GetOwnersByGender(Gender gender)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return Context.Owners.Where(owner => owner.Gender == gender).ToList();
            }
        }

        /// <summary>
        /// Get all possessions that an owner owns
        /// </summary>
        public List<Possession> GetOwnersPossessions(Owner owner)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Possessions.Include(s => s.Owner).Where(s => s.Owner.Id == owner.Id).ToList();
            }
        }

        /// <summary>
        /// Get all the stuff owned by the cats / by the people
        /// </summary>
        public List<Possession> GetPossessionsByOwnerType(OwnerTypes type)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;

                return
                    (from possessions in context.Possessions
                     //join owners in context.Owners 
                     //on possessions.Owner.Id equals owners.Id
                     where possessions.Owner.Type == type
                     select possessions).ToList();
            }
        }

        /// <summary>
        /// Updates the owner from user provided data
        /// </summary>
        public bool UpdateOwner(Owner updated)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                var owner = (from owners in Context.Owners
                            where owners.Id == updated.Id
                            select owners).SingleOrDefault();
                if (OwnerRules.VerifyUpdate(owner, updated))
                {
                    owner.ApplyUpdate(updated);
                }
                return Context.SaveChanges() != 0;
            }
        }

        /// <summary>
        /// Destroys the owner according to user provided data
        /// </summary>
        public bool DestroyOwner(Owner owner)
        {
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                var inDB = Context.Owners.Find(owner.Id);
                var stuffOwned = Context.Possessions.Where(s => s.Owner.Id == owner.Id).ToList();
                foreach(var s in stuffOwned)
                {
                    s.Owner = null;
                }
                Context.SaveChanges();
                Context.Entry(inDB).State = EntityState.Deleted;
                return Context.SaveChanges() != 0;
            }
        }

        public bool IsTableEmpty()
        {
            using (InventoryContext Context = new InventoryContext())
            {
                return !Context.Owners.Any();
            }
        }
    }
}
