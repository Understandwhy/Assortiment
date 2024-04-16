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

namespace Assortiment
{
    /// <summary>
    /// Логика взаимодействия для Администратор.xaml
    /// </summary>
    public partial class Администратор : Window
    {
        Entities entities = new Entities();
        public int Id_user { get; set; }
        public Администратор(string Id_user)

        {
            InitializeComponent();
        }
        private void B_Lichn_Click(object sender, RoutedEventArgs e)
        {
            ЛичныеДанные window = new ЛичныеДанные(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void B_Stori_Click(object sender, RoutedEventArgs e)
        {
            История window = new История(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void B_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }
        private void Otchet_Click(object sender, RoutedEventArgs e)
        {
            Отчёты window = new Отчёты(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }

        private void B_Redact_Click(object sender, RoutedEventArgs e)
        {
            РедактированиеАссортимента window = new РедактированиеАссортимента(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
    }
}
