using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void Product_Status_Test_Normal()
        {
            Chip chip = new Chip("Potato Crisps", 3.05M);

            string expected = "";

            string result = chip.Status;

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void Product_Status_Test_EdgeCases()
        {
            Chip chip = new Chip("Potato Crisps", 3.05M);

            chip.Quantity = 0;

            string expected = "SOLD OUT";

            string result = chip.Status;

            Assert.AreEqual(expected, result);

            chip.Quantity = 1;

            expected = "";

            result = chip.Status;

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void Product_Status_Test_Bounds()
        {
            Chip chip = new Chip("Potato Crisps", 3.05M);

            chip.Quantity = int.MaxValue;

            string expected = "";

            string result = chip.Status;

            Assert.AreEqual(expected, result);

            chip.Quantity = int.MinValue;

            expected = "SOLD OUT";

            result = chip.Status;

            Assert.AreEqual(expected, result);

        }

       


    }
}
