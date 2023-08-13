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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaOrders
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ForCourier(object sender, RoutedEventArgs e)
        {
            var window = new ForCourierWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void ForBaker(object sender, RoutedEventArgs e)
        {
            var window = new ForBakersWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void CreatOrder(object sender, RoutedEventArgs e)
        {
            var window = new CreatOrderWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void ReadyOrders(object sender, RoutedEventArgs e)
        {
            var window = new ReadyOrdersWindow();
            Hide();
            window.ShowDialog();
            Close();
        }
    }
}
