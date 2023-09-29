using System.ComponentModel;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.IO;

namespace Labb_2_2._0
{
    internal class Program
    {
        private static List<Product> products = new List<Product>();

        public static List<Member> users = new List<Member>(); 

        private static Member? loggedInUser;

        private static double totalPrice = 0;
        private static double discountedPrice = 0;
        public static void Stock()
        {
            products.Add(new Product("Fanta", 12));
            products.Add(new Product("Cola", 15));
            products.Add(new Product("Zingo", 10));
            products.Add(new Product("Beer", 20));
            products.Add(new Product("Wine", 27));
            //Add products here
        }
        public static void ExistingUsers()
        {
            //Existing users
            users.Add(new Member("Knatte", "123", Member.Level.Gold));
            users.Add(new Member("Fnatte", "321", Member.Level.Silver));
            users.Add(new Member("Tjatte", "213", Member.Level.Bronze));
            LoadUsers();
        }
        public static void Main(string[] args)
        {
            Stock();
            ExistingUsers();
            WelcomeMenu();
        }
        public static void WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to The Awesome Shop!");
            Console.WriteLine("----MENU----");
            Console.WriteLine("1. SIGN IN");
            Console.WriteLine("2. REGISTER");
            string welcomChoice = Console.ReadLine();
            switch(welcomChoice)
            {
                case "1":
                    LoggIn();
                    break;
                case "2":
                    RegisterUser();
                    break;
                default:
                    Console.WriteLine("Invalid input, press any key");
                    Console.ReadKey();
                    WelcomeMenu();
                    break;
            }

        }
        public static void LoggIn()
        {
            Console.Clear();
            Console.Write("USERNAME: ");
            string username = Console.ReadLine();
            Console.Write("PASSWORD: ");
            string password = Console.ReadLine();

            Member? user = users.FirstOrDefault(u => u.Name == username);
            if(user != null)
            {
                if(user.Password == password)
                {
                    loggedInUser = user;
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Wrong password, try again!");
                    LoggIn();
                }
            }
            else
            {
                Console.WriteLine("User doesnt exist. Press any key to continue to register new user.");
                Console.ReadKey();
                RegisterUser(); 
            }

        }
        public static void RegisterUser()
        {
            Console.Clear();
            Console.Write("CHOOSE A USERNAME: ");
            string usernameInput = Console.ReadLine();
            
            Console.Write("CHOOSE A PASSWORD: ");
            string passwordInput = Console.ReadLine();
            
            Console.WriteLine("Choose a memberlevel:");
            Console.WriteLine("1. Gold");
            Console.WriteLine("2. Silver");
            Console.WriteLine("3. Bronze");
            string levelInput = Console.ReadLine();

            Member.Level level = Member.Level.Bronze;            
            Member? user = users.FirstOrDefault(u => u.Name == usernameInput);
            if (user == null)
            {
                if (levelInput == "1")
                {
                    level = Member.Level.Gold;  
                }
                else if (levelInput == "2")
                {
                    level = Member.Level.Silver;                    
                }
                else if (levelInput == "3")
                {
                    level = Member.Level.Bronze;
                }
                else
                {
                    Console.WriteLine("Invalid input, press any key to try again");
                    Console.ReadKey();
                    RegisterUser();
                }
            }
            else
            {
                Console.WriteLine("User already exist, press any key to choose a new username");
                Console.ReadKey();
                RegisterUser();
            }

            if(!string.IsNullOrEmpty(usernameInput) && !string.IsNullOrEmpty(passwordInput))
            {
                Member newMember = new Member(usernameInput, passwordInput, level);

                string fileName = "Users.txt"; 
                File.AppendAllText(fileName, $"{usernameInput},{passwordInput},{level}\n");
                users.Add(newMember);
            }
            LoggIn();
        }
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {loggedInUser?.Name}!");
            Console.WriteLine("----Menu----");
            Console.WriteLine("1. SHOP");
            Console.WriteLine("2. CART");
            Console.WriteLine("3. CHECKOUT");
            Console.WriteLine("4. ADMIN");
            Console.WriteLine("5. LOGG OUT");
            string menuChoice = Console.ReadLine();

            switch(menuChoice)
            {
                case "1":
                    Shop();
                    break;
                case "2":
                    Cart();
                    break;
                case "3":
                    Checkout();
                    break;
                case "4":
                    Admin();
                    break;
                case "5":
                    loggedInUser.Cart.Clear();
                    loggedInUser = null;
                    WelcomeMenu(); 
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    MainMenu();
                    break;
            }
        }
        public static void Shop()
        {
            Console.Clear();

            Console.WriteLine("------SHOP------");
            Console.WriteLine("Press b to go back to Mainmenu.");

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\t{products[i].Name},\t{products[i].Price} kr.");
                Console.WriteLine("*******************************************************");
            }

            int chooseProduct;
            if (int.TryParse(Console.ReadLine(), out chooseProduct) && chooseProduct >= 1 && chooseProduct <= products.Count)
            {
                Product product = products[chooseProduct - 1];
                CartItem? Found = loggedInUser.Cart.FirstOrDefault(c => c.MatchingItems(product.Name));
                if ( Found != null )
                {
                    Found.quantity++;
                    Console.WriteLine($"{product.Name} added, press any key to continue shopping");
                    Console.ReadKey();
                    Shop();
                }
                else
                {
                    CartItem newCart = new CartItem(product, 1);
                    loggedInUser.Cart.Add(newCart);
                    Console.WriteLine($"{product.Name} added, press any key to continue");
                    Console.ReadKey();
                    Shop();
                }
            }
            else
            {
                Console.WriteLine("Invalid input try again, press b to continue!");
                Console.ReadKey();
                MainMenu();
            }
        }
        public static void Cart()
        {
                
            Console.Clear();
            
            foreach (CartItem item in loggedInUser.Cart)       
            {
                totalPrice += item.ProductItem.Price * item.quantity;            
                Console.WriteLine(item.ToString());
                Console.WriteLine("*************************************");
            }

            discountedPrice = Math.Round(loggedInUser.DecideBonusLevel(totalPrice), 2);
            Console.WriteLine($"Your cart contains: {loggedInUser.Cart.Sum(c=>c.quantity)} products.");
            Console.WriteLine($"Total price: {totalPrice} kr.");
            Console.WriteLine($"Your total price with applied discount: {discountedPrice} kr.");

            Console.WriteLine("1. Continue to checkout");
            Console.WriteLine("2. Back To Mainmenu");
            string lastMenuChoice = Console.ReadLine(); 

            switch(lastMenuChoice)
            {
                case "1":
                    Checkout();
                    break;
                case "2":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    Console.ReadKey();
                    Cart();
                    break;
            }

        }
        public static void Checkout()
        {
            Console.Clear();
            Product.Currencies(Math.Round(discountedPrice, 2));

            Console.WriteLine("Choose paymentmethod");
            Console.WriteLine("1. Swish");
            Console.WriteLine("2. Card");
            Console.WriteLine("3. Back to Mainmenu");
            string paymentMethod = Console.ReadLine();
            switch(paymentMethod)
            {
                case "1":
                    Console.WriteLine("Input code");
                    Console.ReadLine();
                    Console.WriteLine("Purchase accepted, press any key to continue.");
                    Console.ReadKey();
                    Confirmation();
                    break;
                case "2":
                    Console.Write("Input cardnumber: ");
                    Console.ReadLine();
                    Console.Write("Enter pin: ");
                    Console.ReadLine();
                    Confirmation();
                    break;
                case "3":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input, press any key to continue");
                    Console.ReadKey();
                    Checkout();
                    break;

            }
           
        }
        public static void Confirmation()
        {
            Console.Clear();
            Console.WriteLine($"Thanks for your order, confirmation will be sent to: {loggedInUser.Name}@iths.se");
            Console.ReadKey();
            WelcomeMenu();
        }
        public static void Admin()
        {
            
            string password = "Admin123";
            Console.WriteLine("Input Admin password to get access");
            string input = Console.ReadLine();

            Console.Clear();    
            if (input == password)
            {
                foreach (Member member in users)
                {
                    Console.WriteLine(member.ToString());
                }
                    Console.WriteLine("Press any key to go back to Mainmenu");
                    Console.ReadKey();
                    MainMenu();
            }
            else
            {
                Console.WriteLine("Password invalid press any key to continue");
                Console.ReadKey();
                MainMenu();
            }
        }
        public static void LoadUsers()
        {
            string fileName = "Users.txt";
            List<string> acounts = File.ReadAllLines(fileName).ToList();

            foreach(var acount in acounts)
            {
                string[] entries = acount.Split(',');

                if (entries.Length == 3)
                {
                    string Name = entries[0];
                    string Password = entries[1];
                    string levelString = entries[2];

                    if (Enum.TryParse(levelString, out Member.Level level))
                    {
                        Member newMember = new Member(Name, Password, level);
                        users.Add(newMember);
                    }
                }
            }
        }
    }
}