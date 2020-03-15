using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Gum : Product
    {
        /// <summary>
        /// Generates a new Gum object
        /// </summary>
        /// <param name="name">The name of the gum item</param>
        /// <param name="price">The price of the gum item</param>
        public Gum(string name, decimal price) : base(name, price) { }
        /// <summary>
        /// Prints message when gum item is dispensed
        /// </summary>
        /// <returns>The message to be printed</returns>
        public override string MakeSound()
        {
            return "Chew Chew, Yum!";
        }
    }
}
