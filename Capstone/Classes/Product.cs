using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class Product
    {
        /// <summary>
        /// Generates a new Product object and stocks initial quantity to maximum amount (5)
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="price">The price of the item</param>
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
            Quantity = 5;
        }
        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The price of the item
        /// </summary>
        public decimal Price { get; }
        /// <summary>
        /// The remaining quantity of the item stocked in the machine
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The sold out status of the item
        /// </summary>
        public string Status
        {
            get
            {
                return Quantity > 0 ? "" : "SOLD OUT";
            }
        }
        /// <summary>
        /// Prints message when item is dispensed
        /// </summary>
        /// <returns>The message to be printed</returns>
        public virtual string MakeSound()
        {
            return "";
        }
    }
}
