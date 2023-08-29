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

namespace GuessTheNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _level;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(_level < 1)
            {
                MessageBox.Show("Выберите уровень ! ");
                return;
            }
            var window = new GameWindow(_level);
            Hide();
            window.ShowDialog();
            Close();
        }

        private void AboutProgrammButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _level = ((ComboBox)sender).SelectedIndex + 1;
            switch (_level)
            {
                case 0: NumberOfAttemptsLabel.Content = "";break;
                case 1: NumberOfAttemptsLabel.Content = $"Кол-во попыток : 3";break;
                case 2: NumberOfAttemptsLabel.Content = $"Кол-во попыток : 10";break;
                case 3: NumberOfAttemptsLabel.Content = $"Кол-во попыток : 15";break;
            }
        }
    }
}
