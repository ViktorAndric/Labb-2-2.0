using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_2._0
{
    internal class CartItem
    {
        List<Product> items = new List<Product>();

        

        public override string ToString()
        {
            return ($"{items.Count} st, {items.First().Name}, {items.First().Price}, total: {items.Sum(x => x.Price)}");
        }
        public bool MatchingItems(string item)
        {
             return items.First().Equals(item);
        }

        public void AddItem(Product item)
        {
            items.Add(item);
        }

       


    }
}
