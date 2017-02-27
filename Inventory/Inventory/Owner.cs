using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes.Enums;

namespace Inventory.Classes
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OwnerTypes Type { get; set; }
        public List<Stuff> StuffOwned { get; set; }
        public List<Task> TasksToDo { get; set; }
    }
}
