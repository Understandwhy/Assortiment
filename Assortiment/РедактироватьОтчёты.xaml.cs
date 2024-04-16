using System;
using System.Collections.Generic;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;

namespace Assortiment
{
    /// <summary>
    /// Логика взаимодействия для РедактироватьОтчёты.xaml
    /// </summary>
    public partial class РедактироватьОтчёты : Window
    {
        Entities entities = new Entities();
        public int Id_user { get; set; }
        public РедактироватьОтчёты(string Id_user)
        {
            InitializeComponent();
            foreach (var otch in entities.OTCHETI)
            {
                LB_Otcheti.Items.Add(otch);
            }
            foreach (var combo in entities.ASSORTIMENT)
            {
                cmbAss.Items.Add(combo);
            }
        }
        private void LB_Otcheti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedItem = listBox.SelectedItem as OTCHETI;

            if (selectedItem != null)
            {
                txtOstatok.Text = selectedItem.ostatok_na_saite.ToString();
                txtTovar.Text = selectedItem.obyazatelno_na_sclade.ToString();
                txtZakup.Text = selectedItem.zakupit.ToString();
                data_otchet.SelectedDate = selectedItem.data_otchet;
                var selectedAssortment = cmbAss.Items.Cast<ASSORTIMENT>().
                    FirstOrDefault(item => item.Id_assorti == selectedItem.ASSORTIMENT.Id_assorti);
                cmbAss.SelectedItem = selectedAssortment;
            }
            else
            {
                txtOstatok.Text = null;
                txtTovar.Text = null;
                txtZakup.Text = null;
                data_otchet.SelectedDate = null;
                cmbAss.SelectedItem = null;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOstatok.Text) || string.IsNullOrEmpty(txtTovar.Text) ||
                string.IsNullOrEmpty(txtZakup.Text) || cmbAss.SelectedItem == null || data_otchet.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime selectedDate;
            if (!DateTime.TryParse(data_otchet.SelectedDate.ToString(), out selectedDate))
            {
                MessageBox.Show("Неверный формат даты отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedDate.Year > 2024)
            {
                MessageBox.Show("Дата отчета не может быть после 2024 года!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int ostatok, tovar, zakup;
            if (!int.TryParse(txtOstatok.Text, out ostatok) ||
                !int.TryParse(txtTovar.Text, out tovar) ||
                !int.TryParse(txtZakup.Text, out zakup) ||
                tovar < ostatok)
            {
                MessageBox.Show("Поля 'Остаток', 'Товар', 'Закуп' должны содержать только числовые значения, и 'Товар' должен быть больше или равен 'Остатку'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedItem = LB_Otcheti.SelectedItem as OTCHETI;
            try
            {
                if (selectedItem == null)
                {
                    selectedItem = new OTCHETI
                    {
                        data_otchet = selectedDate,
                        zakupit = zakup,
                        obyazatelno_na_sclade = tovar,
                        ostatok_na_saite = ostatok,
                        Id_assorti = (cmbAss.SelectedItem as ASSORTIMENT).Id_assorti
                    };
                    entities.OTCHETI.Add(selectedItem);
                    LB_Otcheti.Items.Add(selectedItem);
                }
                else
                {
                    selectedItem.data_otchet = selectedDate;
                    selectedItem.zakupit = zakup;
                    selectedItem.obyazatelno_na_sclade = tovar;
                    selectedItem.ostatok_na_saite = ostatok;
                    selectedItem.Id_assorti = (cmbAss.SelectedItem as ASSORTIMENT).Id_assorti;
                }
                entities.SaveChanges();
                LB_Otcheti.Items.Refresh();
                MessageBox.Show("Сохранено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                if (selectedItem != null)
                {
                    entities.OTCHETI.Remove(selectedItem);
                    LB_Otcheti.Items.Remove(selectedItem);
                }
            }
        }
        private void B_Vichisl_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOstatok.Text, out int ostatok) && int.TryParse(txtTovar.Text, out int tovar))
            {
                if (tovar >= ostatok)
                {
                    int zakup = tovar - ostatok;
                    txtZakup.Text = zakup.ToString();
                }
                else
                {
                    MessageBox.Show("Значение в поле 'Товар' не может быть меньше значения в поле 'Остаток'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите числовые значения в поля 'Остаток' и 'Товар'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LB_Otcheti.SelectedItem as OTCHETI;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите отчёт для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                entities.OTCHETI.Remove(selectedItem);
                LB_Otcheti.Items.Remove(selectedItem);
                entities.SaveChanges();
                MessageBox.Show("Отчёт удален!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void b_back_Click(object sender, RoutedEventArgs e)
        {
            Главная window = new Главная(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtOstatok.Text = "";
            txtTovar.Text = "";
            txtZakup.Text = "";
            LB_Otcheti.SelectedIndex = -1;
            cmbAss.SelectedIndex = -1;
        }
    }
}




