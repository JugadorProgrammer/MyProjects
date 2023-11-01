using ProductWPF.DataBaseService;
using ProductWPF.DataBaseService.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace ProductWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Product _selectedProduct;
        private ApplicationContext _applicationContext;
        public ObservableCollection<Product> ProductsObservableCollection;

        public MainWindow()
        {
            _applicationContext = new ApplicationContext();
            InitializeComponent();
            ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products);
            dataListBox.ItemsSource = ProductsObservableCollection;
        }


        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            var w = new CreateWindow(_applicationContext);
            w.ShowDialog();
            ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products);
            dataListBox.ItemsSource = ProductsObservableCollection;
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            var w = new EditWindow(_selectedProduct, _applicationContext);
            w.ShowDialog();
            ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products);
            dataListBox.ItemsSource = ProductsObservableCollection;

            sortComboBox.SelectedIndex = 0;
            filtrComboBox.SelectedIndex = 0;
            searchTextBox.Text = "";
        }

        private void dataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProduct = (Product)dataListBox.SelectedItem;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = searchTextBox.Text;
            ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products.ToList().Where(p => p.Name.Contains(name)));
            dataListBox.ItemsSource = ProductsObservableCollection;
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (sortComboBox.SelectedIndex)
            {
                case 0:
                    ProductsObservableCollection = new ObservableCollection<Product>(
                        _applicationContext.Products.ToList()
                        .OrderBy(p => p.Name));
                    break;
                case 1:
                    ProductsObservableCollection = new ObservableCollection<Product>(
                          _applicationContext.Products.ToList()
                        .OrderByDescending(p => p.Name));
                    break;
                case 2:
                    ProductsObservableCollection = new ObservableCollection<Product>(
                        _applicationContext.Products.ToList()
                        .OrderBy(p => p.Price));
                    break;
                case 3:
                    ProductsObservableCollection =
                        new ObservableCollection<Product>(_applicationContext.Products.ToList()
                        .OrderByDescending(p => p.Name));
                    break;
            }
            dataListBox.ItemsSource = ProductsObservableCollection;
        }

        private void filtrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtrComboBox.SelectedIndex == 0)
            {
                ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products);
                dataListBox.ItemsSource = ProductsObservableCollection;
                return;
            }
            var select = ((TextBlock)filtrComboBox.SelectedItem).Text;
            ProductsObservableCollection = new ObservableCollection<Product>(_applicationContext.Products.ToList()
                .Where(p => p.Type.Contains(select)));
            dataListBox.ItemsSource = ProductsObservableCollection;

        }
    }
}
