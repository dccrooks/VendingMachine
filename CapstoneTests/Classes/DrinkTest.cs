using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class DrinkTest
    {
        [TestMethod]
        public void MakeSoundMethod_Normal()
        {
            Drink drink = new Drink("Dr. Salt", 1.50M);

            string sound = drink.MakeSound();

            Assert.AreEqual("Glug Glug, Yum!", sound);

        }

        [TestMethod]
        public void MakeSoundMethod_Exceptions()
        {
            Drink drink = new Drink("", -17M);

            string sound = drink.MakeSound();

            Assert.AreEqual("Glug Glug, Yum!", sound);

            drink = new Drink(null, 0);

            sound = drink.MakeSound();

            Assert.AreEqual("Glug Glug, Yum!", sound);

        }
        

    }
}
