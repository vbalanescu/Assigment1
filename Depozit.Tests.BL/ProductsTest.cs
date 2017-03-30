using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Depozit.BL;
using Depozit.Models;
using System.Linq;
using System.Collections.Generic;
namespace Depozit.UnitTests.Depozit
{
    [TestClass]
    public class TestProducts
    {
        [TestMethod]
        public void GetProducts()
        {
            UserOperations op = new UserOperations();
            IList<Product> list = op.GetProductsForUser();

            Assert.AreEqual(list.ElementAt(0).ID, 1);
        }
    }
}
