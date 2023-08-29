using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GuessTheNumber
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Random _random;
        private int _randomValue;
        private int _numberOfAttempts;
        private int _progressBarValue;
        private DispatcherTimer _timer;
        public GameWindow(in int Level)
        {
            InitializeComponent();
            _random = new Random();
            switch (Level)
            {
                case 1: _randomValue = _random.Next(10); _numberOfAttempts = 3; _progressBarValue = 15 ; break;
                case 2: _randomValue = _random.Next(50); _numberOfAttempts = 10; _progressBarValue = 10; break;
                default : _randomValue = _random.Next(100); _numberOfAttempts = 15; _progressBarValue = 5 ; break;
            }
            TrysLabel.Content = $"Кол-во попыток : {_numberOfAttempts}";
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); 
            _timer.Tick += timer_Tick;
            _timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            GameProgressBar.Value += _progressBarValue;
            if(GameProgressBar.Value == GameProgressBar.Maximum)
            {
                _numberOfAttempts = 0;
                TrysLabel.Content = $"Кол-во попыток : 0";
                _timer.Stop();
                MessageBox.Show("Вы проиграли время кончилось!");
            }
        }
        private void CheckButton_Click(object sender, RoutedEventArgs e) => Check();
        private void Check()
        {
            try
            {
                _timer.Stop();
                if (string.IsNullOrEmpty(ChackTextBox.Text))
                {
                    throw new Exception("Введите число!");
                }
                if (_numberOfAttempts < 1) { MessageBox.Show("Игра окончена!"); return; }
                if(_randomValue == int.Parse(ChackTextBox.Text))
                {
                    TrysLabel.Content = "Вы победили !";
                }
                else
                {
                    --_numberOfAttempts;
                    if( _numberOfAttempts < 1)
                    {
                        _progressBarValue = 0;
                        TrysLabel.Content = $"Кол-во попыток : {_numberOfAttempts}";
                        MessageBox.Show("Вы Проиграли !");
                    }
                    TrysLabel.Content = $"Кол-во попыток : {_numberOfAttempts}";
                    throw new Exception("Вы не угадали !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _timer.Start();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Close();
        }

        private void ChackTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789,".IndexOf(e.Text) < 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Check();
            }
        }
    }
}
