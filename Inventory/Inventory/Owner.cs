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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public OwnerTypes Type { get; set; }

        public override string ToString()
        {
            return $"{Id}\t{FirstName}\t{LastName, 15}\t\t{Birthday.ToShortDateString()}\t{Gender.ToString()}\t{Type.ToString()}";
        }

        public void ApplyUpdate(Owner updated)
        {
            if (updated.FirstName != FirstName)
                FirstName = updated.FirstName;
            if (updated.LastName != LastName)
                LastName = updated.LastName;
            if (updated.Birthday != Birthday)
                Birthday = updated.Birthday;
            if (updated.Gender != Gender)
                Gender = updated.Gender;
            if (updated.Type != Type)
                Type = updated.Type;

        }
        //public virtual List<Stuff> StuffOwned { get; set; }
        //public virtual List<Task> TasksToDo { get; set; }
    }
}
