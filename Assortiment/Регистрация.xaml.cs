using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Assortiment
{
    /// <summary>
    /// Логика взаимодействия для Регистрация.xaml
    /// </summary>
    public partial class Регистрация : Window
    {
        Entities entities = new Entities();
        public Регистрация()
        {

            InitializeComponent();
            {
                cbRol.Items.Add("Администратор");
                cbRol.Items.Add("Менеджер");
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }
        private void btZareg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                USER user = new USER();
                user.name_user = tbName.Text;
                user.surname_user = tbSurname.Text;
                user.login = tbLog.Text;
                user.parol = tbPar.Text;
                if (!string.IsNullOrEmpty(tbPhone.Text) && Regex.IsMatch(tbPhone.Text, @"^\+7\d{10}$"))
                {
                    user.phone = tbPhone.Text;

                    if (cbRol.SelectedItem != null)
                    {
                        user.role = (cbRol.SelectedItem as string);
                    }
                    entities.USER.Add(user);
                    entities.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован: " + tbLog.Text);
                }
                else
                {
                    MessageBox.Show("Некорректный формат номера телефона. Введите номер в формате +7XXXXXXXXXX.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message);
            }
        }
    }
}
