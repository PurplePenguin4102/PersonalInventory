using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.PCL.Classes
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueForCompletion { get; set; }
        public virtual List<Possession> StuffRequired { get; set; }
        public virtual List<Owner> PeopleAssigned { get; set; }
        public bool IsComplete { get; set; }
    }
}
