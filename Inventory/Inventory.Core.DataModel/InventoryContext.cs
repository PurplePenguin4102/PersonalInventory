using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventory.PCL.Classes;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataModel
{
    public class InventoryContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Possession> Possessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}
