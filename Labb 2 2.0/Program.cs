using System.ComponentModel;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace Labb_2_2._0
{
    internal class Program
    {
        private static List<Product> products = new List<Product>();
           
        private static List<User> users = new List<User>();
        
        private static User loggedInUser; 
        
        public static void Main(string[] args)
        {
            products.Add(new Product("Apple", 10));
            products.Add(new Product("Banana", 7));
            products.Add(new Product("Pear", 5));
            products.Add(new Product("Coca Cola", 15));
            products.Add(new Product("Yoghurt", 18));
            //Add products here
            
            users.Add(new User("Knatte", "123"));
            users.Add(new User("Fnatte", "321"));
            users.Add(new User("Tjatte", "213"));
            
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
                    Console.WriteLine("Invalid input");
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

            User? user = users.FirstOrDefault(u => u.Name == username);
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

            
            users.Add(new User (usernameInput, passwordInput));

            LoggIn();
            
        }
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {loggedInUser.Name}!");
            Console.WriteLine("----Menu----");
            Console.WriteLine("1. SHOP");
            Console.WriteLine("2. CART");
            Console.WriteLine("3. CHECKOUT");
            Console.WriteLine("4. LOGG OUT");
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
            for(int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name}, {products[i].Price} kr.");
            }
            
            int chooseProduct = Convert.ToInt32(Console.ReadLine());
            Product product = products[chooseProduct - 1];
            CartItem? Found = loggedInUser.Cart.FirstOrDefault(c => c.MatchingItems(product.Name));
            if ( Found != null )
            {
                Found.AddItem(product);
            }
            else
            {
                CartItem newCart = new CartItem();
                newCart.AddItem(product);
                loggedInUser.Cart.Add(newCart);
            }

            Console.WriteLine("1. Continue Shopping");
            Console.WriteLine("2. Back To Main Menu");
            string shoppinChoice = Console.ReadLine();

            switch(shoppinChoice)
            {
                case "1":
                    Shop();
                    break; 
                case "2":
                    MainMenu();
                    break;
                default: 
                    Console.WriteLine("Invalid input");
                    MainMenu();
                    break;
            }

        }

        public static void BonusSystem()
        {

        }
        public static void Cart()
        {
            Console.Clear();
            double krSum = 0;
            double pundSum = 0;
            double dollarSum = 0;
            double pund = 0;
            double dollar = 0;
            

            foreach (CartItem c in loggedInUser.Cart)
            {
                Console.WriteLine(c);
                
                /*
                krSum += c.Price;
                pundSum += pund;
                dollarSum += dollar;
                pund = c.Price / 13.85;
                dollar = c.Price / 11.1;
                Console.Write($"{c.Name}, {c.Price}kr, ");
                Console.Write($"{Math.Round(pund, 2)}£, ");
                Console.WriteLine($"{Math.Round(dollar, 2)}$");
                
                
            */
            }
            
            pundSum = Math.Round(pundSum, 2);
            dollarSum = Math.Round(dollarSum, 2);

            Console.Write($"Totalt: {krSum} kr.");
            Console.Write($" Totalen i pund: {pundSum}£ ");
            Console.WriteLine($" Totalen i dollar: {dollarSum}");

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
                    Cart();
                    break;
            }

        }
        public static void Checkout()
        {
            Console.Clear();
            Console.WriteLine("Choose paymentmethod");
            Console.WriteLine("1. Swish");
            Console.WriteLine("2. Card");
            Console.WriteLine("3. Back to Mainmenu");
            string paymentMethod = Console.ReadLine();
            switch(paymentMethod)
            {
                case "1":
                    Console.WriteLine("Input phonenumber");
                    Console.ReadLine();
                    Confirmation();
                    break;
                case "2":
                    Console.WriteLine("Input cardnumber");
                    Console.ReadLine();
                    Confirmation();
                    break;
                case "3":
                    MainMenu();
                    break;

            }
           
        }
        public static void Confirmation()
        {
            Console.Clear();
            Console.WriteLine($"Thanks for your order, confirmation will be sent to: {loggedInUser.Name}@iths.se");
            Console.ReadKey();
        }

    }
}