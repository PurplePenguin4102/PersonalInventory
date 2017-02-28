using Inventory.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Interface.FunctionExtensions
{
    public static class Extensions
    {
        public static string ToString(this List<Owner> owners, object throwAway)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var owner in owners)
            {
                sb.Append($"============================{Environment.NewLine}{owner.ToString()}{Environment.NewLine}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
