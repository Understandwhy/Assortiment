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
    /// Логика взаимодействия для Отчёты.xaml
    /// </summary>
    public partial class Отчёты : Window
    {
        Entities entities = new Entities();
        public int Id_user { get; set; }
        public Отчёты(string Id_user)
        {
            InitializeComponent();
            lstView.ItemsSource = entities.OTCHETI.ToList();
            tx_vsego.Text = lstView.Items.Count.ToString();
            tx_naideno.Text = "...";

            cmb_poisk.Items.Add("Все");
            cmb_poisk.Items.Add("больше 5 закупок");
            cmb_poisk.Items.Add("больше 10 закупок");
            cmb_poisk.Items.Add("больше 20 закупок");
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (lstView.SelectedItem == null)
            {
                MessageBox.Show("Выделите запись!");
                return;
            }

            var deletedItem = lstView.SelectedItem as OTCHETI;

            var resalt = MessageBox.Show($"Удалить запись №{deletedItem.Id_otchet}?", "Удаление", MessageBoxButton.YesNo);
            if (resalt == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    entities.OTCHETI.Remove(deletedItem);
                    entities.SaveChanges();

                    lstView.ItemsSource = entities.OTCHETI.ToList();

                    MessageBox.Show("Удаление прошло успешно", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void redactirovat_Click(object sender, RoutedEventArgs e)
        {
            if (Polzovatel.role != "Менеджер")
            {
                MessageBox.Show("Вы не можете это редактировать, так как отчёт уже выложен менеджером!");
                return;
            }
            else
            {
                РедактироватьОтчёты window = new РедактироватьОтчёты(Id_user.ToString());
                this.Close();
                window.ShowDialog();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Администратор adminWindow = new Администратор(Id_user.ToString());
            this.Close();
            adminWindow.Show();
        }
        private void tx_poisk_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tx_poisk.Text))
            {
                var searchValue = tx_poisk.Text.ToLower();
                DateTime searchDate;
                int searchId;

                var isDate = DateTime.TryParse(searchValue, out searchDate);
                var isId = int.TryParse(searchValue, out searchId);

                var items = from item in entities.OTCHETI
                            where isDate && item.data_otchet == searchDate ||
                                  isId && item.Id_assorti == searchId
                            select item;

                lstView.ItemsSource = items.ToList();
                tx_naideno.Text = items.Count().ToString();

                if (items.Count() == 0)
                {
                    MessageBox.Show("Такого отчета нет.");
                }
            }
            else
            {
                lstView.ItemsSource = entities.OTCHETI.ToList();
                tx_naideno.Text = "...";
            }
        }

        private void cmb_poisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                {
                    string selectedGenre = cmb_poisk.SelectedItem.ToString();

                    switch (selectedGenre)
                    {
                        case "Все":
                            lstView.ItemsSource = entities.OTCHETI.ToList();
                            break;
                        case "больше 5 закупок":
                            var zakupkiAbove5 = from otch in entities.OTCHETI
                                                where otch.zakupit > 5
                                                select otch;
                            lstView.ItemsSource = zakupkiAbove5.ToList();
                            break;
                        case "больше 10 закупок":
                            var zakupkiAbove10 = from otch in entities.OTCHETI
                                                 where otch.zakupit > 10
                                                 select otch;
                            lstView.ItemsSource = zakupkiAbove10.ToList();
                            break;
                        case "больше 20 закупок":
                            var zakupkiAbove20 = from otch in entities.OTCHETI
                                                 where otch.zakupit > 20
                                                 select otch;
                            lstView.ItemsSource = zakupkiAbove20.ToList();
                            break;
                        default:
                            MessageBox.Show("Выбрано не то, что нужно.");
                            break;
                    }
                    int foundCount = lstView.Items.Count;
                    tx_naideno.Text = foundCount.ToString();

                    if (foundCount == 0)
                    {
                        MessageBox.Show("Отчётов по данному фильтру нет.");
                    }
                }
            }
        }
    }
}

//            if (tx_poisk.Text != "")
//            {
//                var items = from item in entities.OTCHETI
//                            where item.data_otchet.ToLower().Contains(tx_poisk.Text.ToLower()) ||
//                            item.Id_assorti.ToLower().Contains(tx_poisk.Text.ToLower())
//                            select item;
//                lstView.ItemsSource = items.ToList();
//                tx_naideno.Text = items.Count().ToString();
//                if (items.Count() == 0)
//                {
//                    MessageBox.Show("Такого фильма нет.");
//                }
//                return;
//            }
//            else
//            {
//                lstView.ItemsSource = entities.OTCHETI.ToList();
//                tx_naideno.Text = "...";
//            }
//        }

//    }
//}


