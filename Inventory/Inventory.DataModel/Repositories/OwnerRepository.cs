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

        public static Owner GetOwnerById(int id, string errorMsg = null)
        {
            Owner owner = null;
            using (var Context = new InventoryContext())
            {
                Context.Database.Log = Console.WriteLine;
                owner = Context.Owners.Where(o => o.Id == id).First();
            }
            return owner;
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
                return context.Inventory.Where(s => s.Owner.Id == owner.Id).ToList();
            }
        }

        public static IEnumerable<Owner> GetOwnersStuffByType(OwnerTypes type)
        {
            using (var context = new InventoryContext())
            {
                context.Database.Log = Console.WriteLine;
                return context.Owners
                    .Include("Stuffs")
                    .Where(o => o.Type == type)
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
