using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.PCL.Classes;

namespace Inventory.DataModel.RulesValidation
{
    public static class PossessionRules
    {
        private static List<Func<Possession, Possession, bool>> UpdateRules = new List<Func<Possession, Possession, bool>>
        {
            ((inDB, updated) => inDB.Id == updated.Id)
        };

        public static bool VerifyUpdate(Possession inDB, Possession updated)
        {
            return UpdateRules.All(f => f(inDB, updated));
        }
    }
}
