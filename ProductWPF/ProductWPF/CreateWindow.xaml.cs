using ProductWPF.DataBaseService;
using ProductWPF.DataBaseService.Models;
using System.Windows;

namespace ProductWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        private ApplicationContext _applicationContext;
        public CreateWindow(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            InitializeComponent();
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product();
            p.Type = productTypeTextBox.Text;
            p.Name = productNameTextBox.Text;
            p.Articul = productArticulTextBox.Text;
            p.Material = productMaterialTextBox.Text;
            p.Price = productPriceTextBox.Text;

            _applicationContext.Products.Add(p);

            _applicationContext.SaveChanges();
            MessageBox.Show("Успешно!");
            Hide();
            Close();
        }

        private void CanselButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Close();
        }

        private void productPriceTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = "0123456789,".IndexOf(e.Text) < 0;
        }
    }
}
