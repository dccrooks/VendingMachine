using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class VendingMachineTest
    {
        VendingMachine vendingMachine;

        [TestInitialize]
        public void Initialize()
        {
            vendingMachine = new VendingMachine();
        }

        [TestMethod]
        public void StockMachine_Normal()
        {
            Assert.AreEqual(true, vendingMachine.StockMachine());
        }

        [TestMethod]
        public void DispenseItem_Normal()
        {
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("A11", 0, 0.00M));
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("junk", -5, 10.00M));
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("0", 3, -5.00M));
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("z9", 100, 6.65M));
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("X4", 2, 1.95M));

            Assert.AreEqual("Currently sold out of Stackers.", vendingMachine.DispenseItem("A2", 0, 1.00M));
            Assert.AreEqual("Currently sold out of Heavy.", vendingMachine.DispenseItem("C4", 0, 10.00M));
            Assert.AreEqual("Currently sold out of Chiclets.", vendingMachine.DispenseItem("D3", 0, 5.00M));
            Assert.AreEqual("Currently sold out of Triplemint.", vendingMachine.DispenseItem("D4", 0, 2.00M));
            Assert.AreEqual("Currently sold out of Wonka Bar.", vendingMachine.DispenseItem("B3", 0, 6.65M));

            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("A1", 5, 0.00M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("C4", 1, 0.00M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("D3", 2, 0.50M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("A1", 3, 2.50M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("B1", 4, 1.00M));

            Assert.AreEqual("Dispensing Cola for $1.25. Glug Glug, Yum!", vendingMachine.DispenseItem("C1", 5, 2.00M));
            Assert.AreEqual("Dispensing Moonpie for $1.80. Munch Munch, Yum!", vendingMachine.DispenseItem("B1", 3, 5.00M));
            Assert.AreEqual("Dispensing Stackers for $1.45. Crunch Crunch, Yum!", vendingMachine.DispenseItem("A2", 2, 10.00M));
            Assert.AreEqual("Dispensing Crunchie for $1.75. Munch Munch, Yum!", vendingMachine.DispenseItem("B4", 1, 4.50M));
            Assert.AreEqual("Dispensing Triplemint for $0.75. Chew Chew, Yum!", vendingMachine.DispenseItem("D4", 5, 3.00M));
        }

        [TestMethod]
        public void DispenseItem_EdgeCases()
        {
            //  Sold out edge cases
            Assert.AreEqual("Currently sold out of Cola.", vendingMachine.DispenseItem("C1", 0, 0.00M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("C1", 1, 0.00M));

            Assert.AreEqual("Currently sold out of Heavy.", vendingMachine.DispenseItem("C4", 0, 0.00M));
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("C4", 1, 0.00M));

            Assert.AreEqual("Currently sold out of Heavy.", vendingMachine.DispenseItem("C4", 0, 10.00M));
            Assert.AreEqual("Dispensing Heavy for $1.50. Glug Glug, Yum!", vendingMachine.DispenseItem("C4", 1, 10.00M));

            //  Insufficient funds edge cases
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("A2", 5, 1.44M));
            Assert.AreEqual("Dispensing Stackers for $1.45. Crunch Crunch, Yum!", vendingMachine.DispenseItem("A2", 5, 1.45M));

            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("C1", 1, 1.24M));
            Assert.AreEqual("Dispensing Cola for $1.25. Glug Glug, Yum!", vendingMachine.DispenseItem("C1", 1, 1.25M));

            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("B1", 3, 1.79M));
            Assert.AreEqual("Dispensing Moonpie for $1.80. Munch Munch, Yum!", vendingMachine.DispenseItem("B1", 3, 1.80M));
        }

        [TestMethod]
        public void DispenseItem_Bounds()
        {
            //  Min and max quantity
            Assert.AreEqual("Currently sold out of Stackers.", vendingMachine.DispenseItem("A2", int.MinValue, 10.00M));
            Assert.AreEqual("Dispensing Stackers for $1.45. Crunch Crunch, Yum!", vendingMachine.DispenseItem("A2", int.MaxValue, 10.00M));

            //  Min and max balance
            Assert.AreEqual("Insufficient funds.", vendingMachine.DispenseItem("A2", 5, decimal.MinValue));
            Assert.AreEqual("Dispensing Stackers for $1.45. Crunch Crunch, Yum!", vendingMachine.DispenseItem("A2", 5, decimal.MaxValue));
        }

        [TestMethod]
        public void DispenseItem_Exceptions()
        {
            //  Empty and null slot location
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem("", 0, 0.00M));
            Assert.AreEqual("The product code does not exist.", vendingMachine.DispenseItem(null, 0, 0.00M));
        }

        [TestMethod]
        public void ReturnChange_Normal()
        {
            string expected, result;

            expected = "Quarters: 4 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(1.00M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 3 | Dimes: 1 | Nickels: 1";
            result = vendingMachine.ReturnChange(0.90M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 8 | Dimes: 2 | Nickels: 0";
            result = vendingMachine.ReturnChange(2.20M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 42 | Dimes: 1 | Nickels: 1";
            result = vendingMachine.ReturnChange(10.65M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 0 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(0.00M);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnChange_Weird()
        {
            string expected, result;

            expected = "Quarters: 0 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(-1.00M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 0 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(-10.00M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 0 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(decimal.MinValue);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnChange_EdgeCases()
        {
            string expected, result;

            //  Tests quarters edge
            expected = "Quarters: 3 | Dimes: 2 | Nickels: 0";
            result = vendingMachine.ReturnChange(0.95M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 4 | Dimes: 0 | Nickels: 0";
            result = vendingMachine.ReturnChange(1.00M);
            Assert.AreEqual(expected, result);

            //  Tests dimes edge
            expected = "Quarters: 40 | Dimes: 1 | Nickels: 1";
            result = vendingMachine.ReturnChange(10.15M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 40 | Dimes: 2 | Nickels: 0";
            result = vendingMachine.ReturnChange(10.20M);
            Assert.AreEqual(expected, result);

            //  Tests nickels edge
            expected = "Quarters: 20 | Dimes: 1 | Nickels: 0";
            result = vendingMachine.ReturnChange(5.10M);
            Assert.AreEqual(expected, result);

            expected = "Quarters: 20 | Dimes: 1 | Nickels: 1";
            result = vendingMachine.ReturnChange(5.15M);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WriteLog_Normal()
        {
            Assert.AreEqual(true, vendingMachine.WriteLog());
        }
    }
}
