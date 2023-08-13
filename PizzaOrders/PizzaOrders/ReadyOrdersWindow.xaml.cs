using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaOrders
{
    /// <summary>
    /// Логика взаимодействия для ReadyOrdersWindow.xaml
    /// </summary>
    public partial class ReadyOrdersWindow : Window
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ReadyOrdersWindow()
        {
            Orders = new ObservableCollection<Order>(Order.Orders.Where(i => i.OrderState == OrderState.Ready));
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void Сourier(object sender, RoutedEventArgs e)
        {
            var order = Order.Orders.Where(i=>i.Id == ((Button)sender).TabIndex).FirstOrDefault();
            Order.Orders.Remove(order);
            Orders.Remove(order);
            order.OrderState = OrderState.HandedOverForDelivery;
            Order.Orders.Add(order);
        }


        private void Pickup(object sender, RoutedEventArgs e)
        {
            var order = Order.Orders.Where(i => i.Id == ((Button)sender).TabIndex).FirstOrDefault();
            Order.Orders.Remove(order);
            Orders.Remove(order);
            order.OrderState = OrderState.Paid;
            Order.Orders.Add(order);
        }

        private void Paid(object sender, RoutedEventArgs e)
        {
            var window = new PaidOrdersWindow();
            Hide();
            window.ShowDialog();
            Close();
        }
    }
}
