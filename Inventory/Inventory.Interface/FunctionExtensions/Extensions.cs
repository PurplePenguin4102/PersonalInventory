using Inventory.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI.FunctionExtensions
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

        public static string ToString(this List<Stuff> stuffs, object throwAway)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var stuff in stuffs)
            {
                sb.Append($"============================{Environment.NewLine}{stuff.ToString()}{Environment.NewLine}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
