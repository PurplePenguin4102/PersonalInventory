using System;
using Inventory.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepositoryTests
{
    [TestClass]
    public class PossessionTest
    {
        Possession p1 = new Possession
        {
            Id = 1,
            Name = "Chair",
            Acquired = DateTime.Now,
            Owner = new Owner(),
            Category = Inventory.Classes.Enums.PossessionCategory.Books,
            SubCategory = "Book chair :P"
        };
        Possession p2 = new Possession
        {
            Id = 4,
            Name = "Chair",
            Acquired = DateTime.Now,
            Owner = new Owner(),
            Category = Inventory.Classes.Enums.PossessionCategory.Books,
            SubCategory = "Book chair :P"
        };

        Possession p3 = new Possession
        {
            Id = 4,
            Name = "Chair",
            Acquired = DateTime.Now,
            Owner = new Owner(),
            Category = Inventory.Classes.Enums.PossessionCategory.Books,
            SubCategory = "Book chair :P"
        };

        [TestMethod]
        public void AreEqual()
        {
            Assert.IsTrue(p1.Equals(p2));
        }

        [TestMethod]
        public void HashCodesEqual()
        {
            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
        }
    }
}
