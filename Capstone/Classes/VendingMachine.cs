using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Capstone.Classes;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        /// <summary>
        /// Generates a new VendingMachine object and stocks the machine
        /// </summary>
        public VendingMachine()
        {
            StockMachine();
        }
        /// <summary>
        /// The current balance of the machine
        /// </summary>
        public decimal Balance { get; private set; }
        /// <summary>
        /// The audit log of the machine
        /// </summary>
        public string Audit { get; private set; }
        /// <summary>
        /// The collection of available products, keyed by slot location
        /// </summary>
        public Dictionary<string, Product> Products { get; private set; }

        /// <summary>
        /// Reads the contents of an input file into the collection of available products
        /// and stocks each product to the maximum quantity (5)
        /// </summary>
        /// <returns>Whether the input read was successful</returns>
        public bool StockMachine()
        {
            Products = new Dictionary<string, Product>();
            string filePath = "vendingmachine.csv";

            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] item = sr.ReadLine().Split("|");
                            string itemSlot = item[0];
                            string itemName = item[1];
                            decimal itemPrice = Convert.ToDecimal(item[2]);
                            string itemType = item[3];

                            switch (itemType)
                            {
                                case "Chip":
                                    Products.Add(itemSlot, new Chip(itemName, itemPrice));
                                    break;
                                case "Candy":
                                    Products.Add(itemSlot, new Candy(itemName, itemPrice));
                                    break;
                                case "Drink":
                                    Products.Add(itemSlot, new Drink(itemName, itemPrice));
                                    break;
                                case "Gum":
                                    Products.Add(itemSlot, new Gum(itemName, itemPrice));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failure to read " + filePath);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Failure to open " + filePath);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Display/controller for main menu
        /// </summary>
        public void SelectMainMenu()
        {
            Console.Clear();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("VENDO-MATIC 800 -- MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine();

            switch (SelectMenuOption())
            {
                case "1":
                    DisplayItems(true);
                    SelectMainMenu();
                    break;
                case "2":
                    SelectPurchaseMenu();
                    break;
                case "3":
                    Console.Clear();
                    break;
            }
        }

        /// <summary>
        /// Display/controller for purchase menu
        /// </summary>
        public void SelectPurchaseMenu()
        {
            Console.Clear();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("VENDO-MATIC 800 -- PURCHASE MENU");
            Console.WriteLine();
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: {Balance:C2}");
            Console.WriteLine();

            switch (SelectMenuOption())
            {
                case "1":
                    FeedMoney();
                    SelectPurchaseMenu();
                    break;
                case "2":
                    DisplayItems(false);
                    SelectProduct();
                    SelectPurchaseMenu();
                    break;
                case "3":
                    FinishTransaction();
                    SelectMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Prompts user to select menu option
        /// </summary>
        /// <returns>The user-selected menu option</returns>
        public string SelectMenuOption()
        {
            string selection = "";

            while (selection != "1" && selection != "2" && selection != "3")
            {
                Console.Write("Please select a menu option (1-3): ");
                selection = Console.ReadLine();
            }

            return selection;
        }

        /// <summary>
        /// Displays the collection of available products
        /// </summary>
        /// <param name="pause">Boolean whether the screen should pause for press any key</param>
        public void DisplayItems(bool pause)
        {
            Console.Clear();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("AVAILABLE ITEMS");
            Console.WriteLine();
            Console.Write("Code".PadRight(8));
            Console.Write("Name".PadRight(22));
            Console.Write("Price".PadRight(9));
            Console.Write("Quantity".PadRight(11));
            Console.WriteLine("Status".PadRight(10));
            Console.WriteLine("".PadRight(60, '-'));

            foreach (KeyValuePair<string, Product> product in Products)
            {
                Console.WriteLine(product.Key.PadRight(8)
                    + product.Value.Name.PadRight(22)
                    + "$" + product.Value.Price.ToString().PadRight(8)
                    + product.Value.Quantity.ToString().PadRight(11)
                    + product.Value.Status.PadRight(10)
                );
            }

            Console.WriteLine();

            if (pause)
            {
                Console.WriteLine("".PadRight(60, '='));
                Console.WriteLine();
                Console.WriteLine("Press any key to return to Main Menu");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Allows user to insert dollar bills into the machine
        /// </summary>
        /// <returns>The updated balance of the machine</returns>
        public decimal FeedMoney()
        {
            Console.Clear();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("FEED MONEY");
            Console.WriteLine();

            string bill = "";

            while (bill != "1" && bill != "2" && bill != "5" && bill != "10"
                && bill != "1.00" && bill != "2.00" && bill != "5.00" && bill != "10.00")
            {
                Console.Write("Please insert bills in $1, $2, $5, or $10 denominations: $");
                bill = Console.ReadLine();
            }

            decimal amount = Convert.ToDecimal(bill);
            Balance += amount;
            Audit += $"{DateTime.UtcNow} FEED MONEY: {amount:C2} {Balance:C2}\n";

            return Balance;
        }

        /// <summary>
        /// Allows the user to select and dispense a product if in stock and sufficient funds provided
        /// </summary>
        public void SelectProduct()
        {
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("SELECT PRODUCT");
            Console.WriteLine();

            Console.Write("Please select a product code to purchase: ");
            string selection = Console.ReadLine().ToUpper();
            Console.WriteLine();

            int testableQuantity = Products.ContainsKey(selection) ? Products[selection].Quantity : 0;
            Console.WriteLine(DispenseItem(selection, testableQuantity, Balance));

            Console.WriteLine();
            Console.WriteLine($"Remaining balance: {Balance:C2}");
            Console.WriteLine();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine();
            Console.WriteLine("Press any key to return to Purchase Menu");
            Console.ReadKey();
        }

        /// <summary>
        /// Dispenses an item from the machine
        /// </summary>
        /// <param name="slot">The slot location of the item</param>
        /// <param name="quantity">The quantity of the item</param>
        /// <param name="balance">The balance of the machine</param>
        /// <returns>A message indicating whether the dispense was successful</returns>
        public string DispenseItem(string slot, int quantity, decimal balance)
        {
            string output = "";
            slot = slot != null ? slot : "";

            if (Products.ContainsKey(slot))
            {
                if (quantity > 0)
                {
                    if (balance >= Products[slot].Price)
                    {
                        Products[slot].Quantity--;
                        Balance -= Products[slot].Price;
                        Audit += $"{DateTime.UtcNow} {Products[slot].Name} {slot} {balance:C2} {Balance:C2}\n";

                        output = $"Dispensing {Products[slot].Name} for {Products[slot].Price:C2}. {Products[slot].MakeSound()}";
                    }
                    else
                    {
                        output = "Insufficient funds.";
                    }
                }
                else
                {
                    output = "Currently sold out of " + Products[slot].Name + ".";
                }
            }
            else
            {
                output = "The product code does not exist.";
            }

            return output;
        }

        /// <summary>
        /// Returns change to customer, writes audit to log file, and resets machine balance
        /// </summary>
        public void FinishTransaction()
        {
            Console.Clear();
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine("FINISH TRANSACTION");
            Console.WriteLine();
            Console.WriteLine($"Total change due: {Balance:C2}");
            Console.WriteLine(ReturnChange(Balance));
            Console.WriteLine();

            Audit += $"{DateTime.UtcNow} GIVE CHANGE: {Balance:C2} $0.00\n";
            WriteLog();
            Audit = "";
            Balance = 0.0M;

            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine();
            Console.WriteLine("Press any key to return to Main Menu");
            Console.ReadKey();
        }

        /// <summary>
        /// Returns change amount in quarters, nickels, and dimes
        /// </summary>
        /// <param name="balance">The dollar amount to be converted to change</param>
        /// <returns>Change amount in quarters, nickels, and dimes</returns>
        public string ReturnChange(decimal dollars)
        {
            dollars = dollars >= 0 ? dollars : 0.00M;
            int cents = Convert.ToInt32(dollars * 100);

            int quarterCents = 25;
            int dimeCents = 10;
            int nickelCents = 5;

            int quarters = cents / quarterCents;
            int dimes = (cents % quarterCents) / dimeCents;
            int nickels = ((cents % quarterCents) % dimeCents) / nickelCents;

            return $"Quarters: {quarters} | Dimes: {dimes} | Nickels: {nickels}";
        }

        /// <summary>
        /// Writes machine audit to log file
        /// </summary>
        /// <param name="filePath">The path of the file to be written to</param>
        /// <returns>Whether the write attempt was successful</returns>
        public bool WriteLog()
        {
            string filePath = "Log.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(Audit);
                    sw.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to write to " + filePath);
                return false;
            }

            return true;
        }
    }
}