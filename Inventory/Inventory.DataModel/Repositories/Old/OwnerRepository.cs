using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.Classes.Enums;
using System.Data.Entity;

namespace Inventory.DataModel.Repositories.Old
{
    public static class OwnerRepository
    {
        public static bool CreateOwner(Owner[] newOwner)
        {
            int retVal;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                if (newOwner.Length == 1)
                    Context.Owners.Add(newOwner[0]);
                else
                    Context.Owners.AddRange(newOwner);
                retVal = Context.SaveChanges();
            }
            return retVal != 0;
        }

        public static Owner GetOwnerById(int id)
        {
            using (var Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                return Context.Owners.Find(id);
            }
        }

        public static IEnumerable<Owner> GetAllOwners()
        {
            IEnumerable<Owner> owners = null;
            using (var Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                owners = Context.Owners.ToList();
            }
            return owners;
        }

        public static IEnumerable<Owner> GetOwnerByFirstName(string Name)
        {
            IEnumerable<Owner> FilteredOwner;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                FilteredOwner = Context.Owners.Where(owner => owner.FirstName.ToLower() == Name.ToLower()).ToList();
            }
            return FilteredOwner;
        }

        public static IEnumerable<Owner> GetOwnerByLastName(string Name)
        {
            IEnumerable<Owner> FilteredOwner;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                FilteredOwner = Context.Owners.Where(owner => owner.LastName.ToLower() == Name.ToLower()).ToList();
            }
            return FilteredOwner;
        }

        public static IEnumerable<Owner> GetOwnersByType(OwnerTypes type)
        {
            IEnumerable<Owner> FilteredOwners;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                FilteredOwners = Context.Owners.Where(owner => owner.Type == type).ToList();
            }
            return FilteredOwners;
        }

        public static IEnumerable<Owner> GetOwnersByGender(Gender gender)
        {
            IEnumerable<Owner> FilteredOwners;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                FilteredOwners = Context.Owners.Where(owner => owner.Gender == gender).ToList();
            }
            return FilteredOwners;
        }

        public static IEnumerable<Stuff> GetOwnersStuff(Owner owner)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory.Include(s => s.Owner).Where(s => s.Owner.Id == owner.Id).ToList();
            }
        }

        public static IEnumerable<Stuff> GetOwnersStuffByType(OwnerTypes type)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Inventory
                    .Include(s => s.Owner)
                    .Where(s => s.Owner.Type == type)
                    .ToList();
            }
        }

        public static bool UpdateOwner(Owner updated)
        {
            int retVal;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                Context.Owners.Attach(updated);
                Context.Entry(updated).State = EntityState.Modified;
                retVal = Context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool DestroyOwner(Owner owner)
        {
            int retVal;
            using (InventoryContext Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                Context.Owners.Attach(owner);
                var stuffOwned = Context.Inventory.Where(s => s.Owner.Id == owner.Id).ToList();
                foreach(var s in stuffOwned)
                {
                    s.Owner = null;
                    
                }
                Context.SaveChanges();
                Context.Entry(owner).State = EntityState.Deleted;
                retVal = Context.SaveChanges();
            }
            return retVal != 0;
        }

        public static bool IsTableEmpty()
        {
            bool empty;
            using (InventoryContext Context = new InventoryContext())
            {
                empty = !Context.Owners.Any();
            }
            return empty; 
        }
    }
}
