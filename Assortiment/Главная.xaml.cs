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
    /// Логика взаимодействия для Главная.xaml
    /// </summary>
    public partial class Главная : Window
    {
        Entities entities = new Entities();
        public int Id_user { get; set; }
        public Главная(string Id_user)
        {
            InitializeComponent();
        }
        private void Button_Otcheti_Click(object sender, RoutedEventArgs e)
        {
            РедактироватьОтчёты window = new РедактироватьОтчёты(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void Button_Lichn_Click(object sender, RoutedEventArgs e)
        {
            ЛичныеДанные window = new ЛичныеДанные(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }
        private void Button_Asort_Click_1(object sender, RoutedEventArgs e)
        {
            Ассортимент window = new Ассортимент(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
    }
}
