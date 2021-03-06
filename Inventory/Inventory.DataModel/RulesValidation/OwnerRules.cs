﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.PCL.Classes;

namespace Inventory.DataModel.RulesValidation
{
    public static class OwnerRules
    {
        private static List<Func<Owner, bool>> InsertRules = new List<Func<Owner, bool>>
        {
            ((owner1) => owner1.LastName.Length < 50),
            ((owner1) => owner1.FirstName.Length < 50),
            ((owner1) => owner1.Birthday > new DateTime(1930, 1, 1))
        };

        private static List<Func<Owner, Owner, bool>> UpdateRules = new List<Func<Owner, Owner, bool>>
        {
            ((owner1, owner2) => owner1.Type == owner2.Type),
            ((owner1, owner2) => owner2.LastName.Length < 50),
            ((owner1, owner2) => owner2.FirstName.Length < 50),
            ((owner1, owner2) => owner2.Birthday > new DateTime(1930, 1, 1))
        };

        public static bool VerifyUpdate(Owner updated)
        {
            return InsertRules.All(f => f(updated));
        }

        public static bool VerifyInsert(Owner newGuy)
        {
            return InsertRules.All(f => f(newGuy));
        }
    }
}
