using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Classes
{
    public class Stuff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Owner Owner { get; set; }
        public bool InUse { get; set; }
        public List<Stuff> Contents { get; set; }
    }
}
