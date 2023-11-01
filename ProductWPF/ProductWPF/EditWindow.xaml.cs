using ProductWPF.DataBaseService;
using ProductWPF.DataBaseService.Models;
using System.Windows;

namespace ProductWPF
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Product _product;
        private ApplicationContext _applicationContext;
        public EditWindow(Product p, ApplicationContext applicationContext)
        {
            _product = p;
            _applicationContext = applicationContext;
            InitializeComponent();
            if (p != null)
            {
                productTypeTextBox.Text = p.Type;
                productNameTextBox.Text = p.Name;
                productArticulTextBox.Text = p.Articul;
                productMaterialTextBox.Text = p.Material;
                productPriceTextBox.Text = p.Price;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _product.Type = productTypeTextBox.Text;
            _product.Name = productNameTextBox.Text;
            _product.Articul = productArticulTextBox.Text;
            _product.Material = productMaterialTextBox.Text;
            _product.Price = productPriceTextBox.Text;

            _applicationContext.Products.Remove(_product);
            _applicationContext.SaveChanges();
            MessageBox.Show("Успешно!");
            Hide();
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _product.Type = productTypeTextBox.Text;
            _product.Name = productNameTextBox.Text;
            _product.Articul = productArticulTextBox.Text;
            _product.Material = productMaterialTextBox.Text;
            _product.Price = productPriceTextBox.Text;

            _applicationContext.Products.Update(_product);
            _applicationContext.SaveChanges();
            MessageBox.Show("Успешно!");
            Hide();
            Close();
        }

        private void productPriceTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = "0123456789,".IndexOf(e.Text) < 0;
        }
    }
}
