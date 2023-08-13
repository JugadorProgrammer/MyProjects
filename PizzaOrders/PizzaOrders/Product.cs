using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrders
{
    public class Product
    {
        public Product(string name, params KeyValuePair<string, int>[] pairs)
        {
            Ingreedients = new Dictionary<string, int>();
            Name = name;
            for (int i = 0; i < pairs.Length; i++) { Ingreedients.Add(pairs[i].Key, pairs[i].Value); }
        }
        public string Name { get; set; }
        public bool IsDrink { get; set; }
        public Dictionary<string, int> Ingreedients { get; set; }
    }
}
