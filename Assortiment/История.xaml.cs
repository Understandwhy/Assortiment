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
    /// Логика взаимодействия для История.xaml
    /// </summary>
    public partial class История : Window
    {
        Entities entities = new Entities();
        public int Id_user { get; set; }
        public История(string Id_user)
        {
            InitializeComponent();
            foreach (var POLZ in entities.USER)
                LbPolzov.Items.Add(POLZ);
            foreach (USER_HISTIRY vhod in entities.USER_HISTIRY)
            {
                VHOD.Items.Add(vhod.time_VHOD);
                VIHOD.Items.Add(vhod.time_VIHOD);
            }
            VIHOD.Items.Clear();
            VHOD.Items.Clear();
        }
        private void LbHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            USER selectedUs = (USER)LbPolzov.SelectedItem;

            if (selectedUs != null)
            {
                VHOD.Items.Clear();
                VIHOD.Items.Clear();
                var story = entities.USER_HISTIRY.Where(v => v.Id_user == selectedUs.Id_user);
                foreach (var entry in story)
                {
                    VHOD.Items.Add(entry.time_VHOD);
                    VIHOD.Items.Add(entry.time_VIHOD);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Администратор window = new Администратор(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }

    }
}

