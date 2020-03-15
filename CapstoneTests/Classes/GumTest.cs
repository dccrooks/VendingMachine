using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests.Classes
{
    [TestClass]
    public class GumTest
    {
        [TestMethod]
        public void MakeSoundMethod_Normal()
        {
            Gum gum  = new Gum("Chiclets", 0.75M);

            string sound = gum.MakeSound();

            Assert.AreEqual("Chew Chew, Yum!", sound);

        }
        [TestMethod]
        public void MakeSoundMethod_Exceptions()
        {
            Gum gum = new Gum("", -15M);

            string sound = gum.MakeSound();

            Assert.AreEqual("Chew Chew, Yum!", sound);

            gum = new Gum(null, 0);

            sound = gum.MakeSound();

            Assert.AreEqual("Chew Chew, Yum!", sound);

        }

        
    }
}
