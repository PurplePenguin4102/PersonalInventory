using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Inventory.Classes;

namespace Inventory.DataModel
{
    public class InventoryContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Possession> Possessions { get; set; }
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<Task> ToDoList { get; set; }
        //public DbSet<History> History { get; set; }

        public InventoryContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<InventoryContext>());
        }
    }
}
