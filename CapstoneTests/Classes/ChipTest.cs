using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests.Classes
{

    [TestClass]
    public class ChipTest
    {
        [TestMethod]
        public void MakeSoundMethod_Normal()
        {
            Chip chip = new Chip("Potato Crisps", 3.05M);

            string sound = chip.MakeSound();

            Assert.AreEqual("Crunch Crunch, Yum!", sound);

        }
        [TestMethod]
        public void MakeSoundMethod_Exceptions()
        {
            Chip chip = new Chip("", -3.05M);

            string sound = chip.MakeSound();

            Assert.AreEqual("Crunch Crunch, Yum!", sound);
            
            chip = new Chip(null, 0M);

            sound = chip.MakeSound();

            Assert.AreEqual("Crunch Crunch, Yum!", sound);

        }

       
    }
}
