using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class CartItem
    {
        public Product ProductItem{ get; private set; }
        public int quantity { get; set;} = 0;
        public double totalItem = 0;
        public override string ToString()
        {
            return $"{quantity} st\t {ProductItem.Name} \t{ProductItem.Price} \ttotal: {ProductItem.Price * quantity}";  
        }

        public bool MatchingItems(string item)
        {
            return this.ProductItem.Name.Equals(item);
        }

        public CartItem(Product item, int Quantity)
        {
            this.ProductItem = item;
            quantity = Quantity;
        }
        
    }
}
