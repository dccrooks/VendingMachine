using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chip : Product
    {
        /// <summary>
        /// Generates a new Chip object
        /// </summary>
        /// <param name="name">The name of the chip item</param>
        /// <param name="price">The price of the chip item</param>
        public Chip(string name, decimal price) : base(name, price) { }
        /// <summary>
        /// Prints message when chip item is dispensed
        /// </summary>
        /// <returns>The message to be printed</returns>
        public override string MakeSound()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
