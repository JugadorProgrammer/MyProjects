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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public List<Product> Products;
        public List<Product> MenuProducts = new List<Product>()
        {
            new Product("Пеперони",new KeyValuePair<string, int>("Тесто",1),new KeyValuePair<string, int>("Сыр",1),new KeyValuePair<string, int>("Пеперонни",1)),
            new Product("Маргарита",new KeyValuePair<string, int>("Тесто",1),new KeyValuePair<string, int>("Сыр",1),new KeyValuePair<string, int>("Соус",1)),
            new Product("4 сыра",new KeyValuePair<string, int>("Тесто",1),new KeyValuePair<string, int>("Сыр1",1),new KeyValuePair<string, int>("Сыр2",1)),
            new Product("Мохито",new KeyValuePair<string, int>("Вода",100),new KeyValuePair<string, int>("Водка",50),new KeyValuePair<string, int>("Мята",20)){ IsDrink = true}
        };
        public AddProductWindow()
        {
            InitializeComponent();
            Products = new List<Product>();
            string[] dishes = new string[MenuProducts.Count];
            for (int i = 0; i < MenuProducts.Count;++i)
            {
                dishes[i] = MenuProducts[i].Name;
            }
            OrderProductsListBox.ItemsSource = dishes;
        }
        public AddProductWindow(List<Product> products)
        {
            InitializeComponent();
            this.Products = products;
            OrderProductsListBox.ItemsSource = Products;
            string[] dishes = new string[MenuProducts.Count];
            for (int i = 0; i < MenuProducts.Count; ++i)
            {
                dishes[i] = MenuProducts[i].Name;
            }
            OrderProductsListBox.ItemsSource = dishes;
            dishes = new string[Products.Count];
            for (int i = 0; i < Products.Count; ++i)
            {
                dishes[i] = Products[i].Name;
            }
            ProductsListBox.ItemsSource = dishes;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var window = new CreatOrderWindow(Products);
            Hide();
            window.ShowDialog();
            Close();
        }

        private void ProductsListBox_Selected(object sender, MouseButtonEventArgs e)
        {
            if (ProductsListBox.SelectedItem != null)
            {
                Products.Remove(Products.Where(i => ProductsListBox.SelectedItem.ToString().Contains(i.Name)).FirstOrDefault());
                ProductsListBox.Items.Remove(ProductsListBox.SelectedItem);
            }
            
        }

        private void OrderProductsListBox_Selected(object sender, MouseButtonEventArgs e)
        {
            if (OrderProductsListBox.SelectedItem != null)
            {
                Products.Add(MenuProducts.Where(i => OrderProductsListBox.SelectedItem.ToString().Contains(i.Name)).FirstOrDefault());
                ProductsListBox.Items.Add(OrderProductsListBox.SelectedItem);
            }
                
        }
    }
}
