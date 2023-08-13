using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrders
{
    
    public enum OrderState
    {
        New,
        InProduction,
        Accepted,
        Ready,
        Paid,
        HandedOverForDelivery
    }
    public class Order
    {
        public static int NextId { get; set; } = 0;
        public static List<Order> Orders { get; set; } = new List<Order>();

        public int Id { get; set; }
        public List<Product> products { get; set; }
        public TimeSpan OrderCreationTime { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string FIO { get; set; }
        public string  Phone { get; set; }
        public string  Adress { get; set; }
        public string PaymentMethod { get; set; }
        public OrderState OrderState { get; set; }
        public string ResponsibleBakerName { get; set; } = "-";

        public Order()
        {
            ++NextId;
            this.Id = NextId;
        }

        public string DrinksToString
        {
            get {
                string str = "";
                foreach (var item in products)
                {
                    if (item.IsDrink)
                    {
                        str += $"{item.Name}; ";
                    }
                }
                return str;
            }
            
        }

        public string DishToString
        {
            get
            {
                string str = "";
                foreach (var item in products)
                {
                    if (!item.IsDrink)
                    {
                        str += $"{item.Name}; ";
                    }
                }
                return str;
            }
        }
    }


}
