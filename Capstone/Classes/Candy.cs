using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy : Product
    {
        /// <summary>
        /// Generates a new Candy object
        /// </summary>
        /// <param name="name">The name of the candy item</param>
        /// <param name="price">The price of the candy item</param>
        public Candy(string name, decimal price) : base(name, price) { }
        /// <summary>
        /// Prints message when candy item is dispensed
        /// </summary>
        /// <returns>The message to be printed</returns>
        public override string MakeSound()
        {
            return "Munch Munch, Yum!";
        }
    }
}
