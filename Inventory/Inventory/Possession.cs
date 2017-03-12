using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes.Enums;

namespace Inventory.Classes
{
    public class Possession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Acquired { get; set; }
        //public virtual Location Location { get; set; }
        public Owner Owner { get; set; }
        public PossessionCategory Category { get; set; }
        public string SubCategory { get; set; }
        public bool InUse { get; set; }
        public Possession PartOf { get; set; }
        //public virtual List<Stuff> Contents { get; set; }
        //public virtual List<Task> RequiredFor { get; set; }

        public override bool Equals(object obj)
        {
            var possession = obj as Possession;

            if (possession == null)
                return base.Equals(obj);

            bool ownersEqual;
            if (Owner != null)
                ownersEqual = Owner.Equals(possession.Owner);
            else 
                ownersEqual = possession.Owner == null;

            bool possessionsEqual;
            if (PartOf != null)
                possessionsEqual = PartOf.Equals(possession.PartOf);
            else
                possessionsEqual = possession.PartOf == null;

            return this.Name == possession.Name &&
                this.Acquired == possession.Acquired &&
                this.Category == possession.Category &&
                this.SubCategory == possession.SubCategory &&
                this.InUse == possession.InUse &&
                ownersEqual && possessionsEqual;
        }

        public override int GetHashCode()
        {
            int ownerHashCode = (Owner != null) ? Owner.GetHashCode() : 0;
            int partOfHashCode = (PartOf != null) ? PartOf.GetHashCode() : 0;
            return Name.GetHashCode()
                + Acquired.GetHashCode()
                + Category.GetHashCode()
                + SubCategory.GetHashCode()
                + InUse.GetHashCode()
                + ownerHashCode
                + partOfHashCode;
        }

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

        private enum ToStringOptions
        {
            NoPartof = 0x01,
            NoOwner = 0x02,
        }

        public void ApplyUpdate(Possession updated)
        {
            Name = updated.Name;
            this.Owner = updated.Owner;
            this.InUse = updated.InUse;
            this.PartOf = updated.PartOf;
            this.SubCategory = updated.SubCategory;
            this.Category = updated.Category;
            this.Acquired = updated.Acquired;
        }
    }

    
}
