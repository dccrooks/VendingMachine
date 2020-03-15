using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Drink : Product
    {
        /// <summary>
        /// Generates a new Drink object
        /// </summary>
        /// <param name="name">The name of the drink item</param>
        /// <param name="price">The price of the drink item</param>
        public Drink(string name, decimal price) : base(name, price) { }
        /// <summary>
        /// Prints message when drink item is dispensed
        /// </summary>
        /// <returns>The message to be printed</returns>
        public override string MakeSound()
        {
            return "Glug Glug, Yum!";
        }
    }
}
