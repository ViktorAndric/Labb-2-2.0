using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class User
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        private List<CartItem> _cart;
        public List<CartItem> Cart { get { return _cart; } }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
            _cart = new List<CartItem>();
        }

        public void BonusSystem()
        {

        }
    }
}
