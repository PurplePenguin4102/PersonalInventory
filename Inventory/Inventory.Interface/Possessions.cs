using Inventory.PCL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI
{
    class Possessions
    {
        public List<Possession> PossessiontList { get; set; }
        
        public static implicit operator List<Possession>(Possessions p) => p.PossessiontList;
        public static implicit operator Possessions(List<Possession> p) => new Possessions { PossessiontList = p };

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var stuff in PossessiontList)
            {
                sb.Append($"============================{Environment.NewLine}{stuff.ToString()}{Environment.NewLine}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
