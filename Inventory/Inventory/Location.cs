using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes.Enums;

namespace Inventory.Classes
{
    public class Location
    {
        public int Id { get; set; }
        public Rooms Room { get; set; }
        public StorageType TypeOfStorage { get; set; }
        public virtual List<Stuff> StuffStored { get; set; }
    }
}
