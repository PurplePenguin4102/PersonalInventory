using Inventory.PCL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI
{
    class Owners
    {
        public List<Owner> OwnersList { get; set; }

        public static implicit operator List<Owner>(Owners p) => p.OwnersList;
        public static implicit operator Owners(List<Owner> p) => new Owners { OwnersList = p };

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var owner in OwnersList)
            {
                sb.Append($"============================{Environment.NewLine}{owner.ToString()}{Environment.NewLine}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
