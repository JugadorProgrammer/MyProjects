using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PaidOrdersWindow.xaml
    /// </summary>
    public partial class PaidOrdersWindow : Window
    {
        public IEnumerable<Order> Orders { get; }
        public PaidOrdersWindow()
        {
            Orders = Order.Orders.Where(i => i.OrderState == OrderState.Paid);
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var window = new ReadyOrdersWindow();
            Hide();
            window.ShowDialog();
            Close();
        }
    }
}
