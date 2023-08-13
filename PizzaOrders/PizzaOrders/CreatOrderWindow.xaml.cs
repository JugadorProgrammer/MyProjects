using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CreatOrderWindow.xaml
    /// </summary>
    public partial class CreatOrderWindow : Window
    {
        private Order order = new Order();
        public CreatOrderWindow(List<Product> products)
        {
            InitializeComponent();
            order.products = products;
            foreach (var item in products)
            {
                if (item.IsDrink)
                {
                    drinkTextBox.Text += $"{item.Name}; ";
                }
                else
                {
                    dishTextBox.Text += $"{item.Name}; ";
                }
            }
        }
        public CreatOrderWindow()
        {
            InitializeComponent();
            order.products = new List<Product>();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FIOTextBox.Text)
                || string.IsNullOrEmpty(phoneTextBox.Text)
                || PaymentMethodComboBox.SelectedIndex == -1
                || string.IsNullOrEmpty(adressTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            order.OrderCreationTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            order.OrderState = OrderState.New;
            order.DeliveryTime = new TimeSpan(1,0,0) + order.OrderCreationTime;
            order.Adress = adressTextBox.Text;
            order.Phone = phoneTextBox.Text;
            order.FIO = FIOTextBox.Text;
            order.PaymentMethod = PaymentMethodComboBox.Text.ToString();

            var Result = MessageBox.Show("Хотите создать новый заказ?","Создать заказ!",MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Order.Orders.Add(order);
                var window = new MainWindow();
                Hide();
                window.ShowDialog();
                Close();
            }
        }

        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "+()0123456789 -".IndexOf(e.Text) < 0;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            var window = new AddProductWindow();
            Hide();
            window.ShowDialog();
            Close();
        }
    }
}
