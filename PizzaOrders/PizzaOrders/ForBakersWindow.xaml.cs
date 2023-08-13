using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Логика взаимодействия для ForBakersWindow.xaml
    /// </summary>
    public partial class ForBakersWindow : Window
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ForBakersWindow()
        {
            Orders = new ObservableCollection<Order>(Order.Orders.Where(i => i.OrderState == OrderState.InProduction || i.OrderState == OrderState.New).ToList());
            InitializeComponent();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void ReadyOrder(object sender, RoutedEventArgs e)
        {
            var order = Order.Orders.Where(i => i.Id == ((Button)sender).TabIndex).FirstOrDefault();
            if (order.OrderState != OrderState.InProduction)
            {
                MessageBox.Show("Заказ не принят в обрабоку!"); return;
            }

            Order.Orders.Remove(order);
            Orders.Remove(order);
            order.OrderState = OrderState.Ready;
            Order.Orders.Add(order);
        }

        private void AcceptOrder(object sender, RoutedEventArgs e)
        {
            var order = Order.Orders.Where(i => i.Id == ((Button)sender).TabIndex).FirstOrDefault();
            if (order.OrderState != OrderState.New)
            {
                return;
            }
            if (string.IsNullOrEmpty(FIOTextBox.Text))
            {
                MessageBox.Show("Заполните имя повара!"); return;
            }

            Order.Orders.Remove(order);
            Orders.Remove(order);

            order.ResponsibleBakerName = FIOTextBox.Text;
            order.OrderState = OrderState.InProduction;

            Order.Orders.Add(order);
            Orders.Add(order);
        }
    }
}
