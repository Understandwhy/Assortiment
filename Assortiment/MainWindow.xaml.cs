using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assortiment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        private System.Threading.Timer timer;
        private int seconds = 0;
        public MainWindow()
        {
            InitializeComponent();
            GridCaptcha.Visibility = Visibility.Hidden;
            timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (
              TextBox_Login.Text == "" ||
              PasswordBox_Password.Password == ""
              )
            {
                MessageBox.Show("Заполните все поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool flag = false;
                foreach (var log in entities.USER)
                {
                    if (TextBox_Login.Text == log.login)
                    {
                        if (PasswordBox_Password.Password == log.parol)
                        {
                            switch (log.role)
                            {
                                case ("Администратор"):
                                    flag = true;
                                    MessageBox.Show("Вы вошли как Администратор");
                                    var window = new Администратор(log.Id_user.ToString());
                                    Close();
                                    window.ShowDialog();
                                    break;

                                case ("Менеджер"):
                                    flag = true;
                                    MessageBox.Show("Вы вошли как Менеджер");
                                    var window1 = new Главная(log.Id_user.ToString());
                                    Close();
                                    window1.ShowDialog();
                                    break;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    GridCaptcha.Visibility = Visibility.Visible;
                    GridAuto.Visibility = Visibility.Hidden;
                    sozdenie();
                }
            }
        }
        private void TimerCallback(Object o)
        {
            Button_Vhod.Dispatcher.Invoke(() =>
            {
                Button_Vhod.IsEnabled = true;
                TextBox_Login.BorderBrush = Brushes.Black;
                PasswordBox_Password.BorderBrush = Brushes.Black;
            });
        }
        public void sozdenie()
        {
            Random rand = new Random();
            int num = rand.Next(6, 8);
            string captcha = "";
            int tot1 = 0;
            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    captcha = captcha + (char)chr;
                    tot1++;
                    if (tot1 == num)
                        break;
                }
            }
            while (true);
            Label_Captcha.Content = captcha;
        }

        private void Button_Check_Click_1(object sender, RoutedEventArgs e)
        {
            if (TextBox_Prverka.Text == Label_Captcha.Content.ToString())
            {


                MessageBox.Show("Неверный логин или пароль,попробуйте снова через 10 секунд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                Button_Vhod.IsEnabled = false; // Делаем кнопку недоступной
                Thread.Sleep(10000); // Устанавливаем таймер на 10 секунд
                TextBox_Login.BorderBrush = Brushes.Red;
                PasswordBox_Password.BorderBrush = Brushes.Red;
                GridCaptcha.Visibility = Visibility.Hidden;
                GridAuto.Visibility = Visibility.Visible;
                Button_Vhod.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("Неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                TextBox_Prverka.Clear();
                sozdenie();
            }
        }
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            Регистрация window = new Регистрация();
            this.Close();
            window.ShowDialog();
        }
    }
}
