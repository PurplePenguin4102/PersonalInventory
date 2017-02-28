using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes.Enums;

namespace Inventory.Classes
{
    public class Stuff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Acquired { get; set; }
        //public virtual Location Location { get; set; }
        public virtual Owner Owner { get; set; }
        public StuffCategory Category { get; set; }
        public string SubCategory { get; set; }
        public bool InUse { get; set; }
        public Stuff PartOf { get; set; }
        //public virtual List<Stuff> Contents { get; set; }
        //public virtual List<Task> RequiredFor { get; set; }
    }
}
