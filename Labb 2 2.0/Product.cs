using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Product(string name, double price)
        {
            Name = name;
            Price = price;           
        }


        public static void Currencies(double price)
        {
            double pund = price / 13.85;
            double dollar = price / 11.1;
            

            Console.WriteLine($"Which currencie would you like to pay with?");
            Console.WriteLine($"1. Kronor: {price}");
            Console.WriteLine($"2. Pund: {Math.Round(pund, 2)}");
            Console.WriteLine($"3. Dollar: {Math.Round(dollar, 2)}");
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine($"Total price is: {price} kr");
                    break;
                case "2":
                    Console.WriteLine($"Total price is: {Math.Round(pund, 2)} £");
                    break; 
                case "3":
                    Console.WriteLine($"Total price is: {Math.Round(dollar, 2)} $");
                    break;
            }
            

        }
    }
}
