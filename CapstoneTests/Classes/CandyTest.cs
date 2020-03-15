using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;


namespace CapstoneTests.Classes
{
    [TestClass]
    public class CandyTest
    {
        [TestMethod]
        public void MakeSoundMethod_Normal()
        {
            Candy candy = new Candy("Crunchie", 1.75M);

            string sound = candy.MakeSound();

            Assert.AreEqual("Munch Munch, Yum!", sound);

        }
        [TestMethod]
        public void MakeSoundMethod_Exceptions()
        {
            Candy candy = new Candy("", -1.75M);

            string sound = candy.MakeSound();

            Assert.AreEqual("Munch Munch, Yum!", sound);

            candy = new Candy(null, -1.0M);

            sound = candy.MakeSound();

            Assert.AreEqual("Munch Munch, Yum!", sound);

        }

        
    }
}
