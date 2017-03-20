using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.PCL.Classes.Enums;

namespace Inventory.PCL.Classes
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
            var hash = Name == null ? 0 : Name.GetHashCode();
            hash += Acquired.GetHashCode();
            hash += Category.GetHashCode();
            hash += SubCategory == null ? 0 : SubCategory.GetHashCode();
            hash += InUse.GetHashCode();
            hash += Owner == null ? 0 : Owner.GetHashCode();
            hash += PartOf == null ? 0 : PartOf.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{Id}\t{Name, -20}\t\t{Acquired.ToString("D")}\t\t{Category.ToString()}\t{SubCategory}\t{InUse}");
            if (PartOf != null)
                sb.Append($"\r\nPart of :\r\n{PartOf.ToMiniString()}");
            if (Owner != null)
                sb.Append($"\r\nOwner\r\n{Owner}");
            return sb.ToString();
        }

        private string ToMiniString()
        {
            return $"{Id}\t{Name}\t\t{Acquired.ToString("D")}\t\t{Category.ToString()}\t{SubCategory}\t{InUse}";
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
