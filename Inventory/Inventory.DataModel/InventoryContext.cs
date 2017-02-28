using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Inventory.Classes;

namespace Inventory.DataModel
{
    public class InventoryContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        //public DbSet<Stuff> Inventory { get; set; }
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<Classes.Task> ToDoList { get; set; }
        //public DbSet<History> History { get; set; }

    }
}
