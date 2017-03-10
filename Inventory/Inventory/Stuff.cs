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
        public Owner Owner { get; set; }
        public StuffCategory Category { get; set; }
        public string SubCategory { get; set; }
        public bool InUse { get; set; }
        public Stuff PartOf { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{Id}\t{Name, -20}\t\t{Acquired.ToShortDateString()}\t\t{Category.ToString()}\t{SubCategory}\t{InUse}");
            if (PartOf != null)
                sb.Append($"\r\nPart of :\r\n{PartOf.ToString(ToStringOptions.NoPartof | ToStringOptions.NoOwner)}");
            if (Owner != null)
                sb.Append($"\r\nOwner\r\n{Owner}");
            return sb.ToString();
        }

        private string ToString(ToStringOptions tso)
        {
            StringBuilder sb = new StringBuilder($"{Id}\t{Name}\t\t{Acquired.ToShortDateString()}\t\t{Category.ToString()}\t{SubCategory}\t{InUse}");
            if (PartOf != null && ((int)tso & 0x01) != (int)ToStringOptions.NoPartof)
                sb.Append($"\r\nPart of :\r\n{PartOf}");
            if (Owner != null && ((int)tso & 0x02) != (int)ToStringOptions.NoOwner)
                sb.Append($"\r\nOwner\r\n{Owner}");
            return sb.ToString();
        }
        //public virtual List<Stuff> Contents { get; set; }
        //public virtual List<Task> RequiredFor { get; set; }
        private enum ToStringOptions
        {
            NoPartof = 0x01,
            NoOwner = 0x02,
        }
    }

    
}
