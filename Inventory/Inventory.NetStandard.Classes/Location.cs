using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.PCL.Classes.Enums;

namespace Inventory.PCL.Classes
{
    public class Location
    {
        public int Id { get; set; }
        public Rooms Room { get; set; }
        public StorageType TypeOfStorage { get; set; }
        public virtual List<Possession> StuffStored { get; set; }
    }
}
