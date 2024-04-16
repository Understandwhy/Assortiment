using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для Ассортимент.xaml
    /// </summary>
    public partial class Ассортимент : Window
    {
        Entities entities = new Entities();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public int Id_user { get; set; }

        public Ассортимент(string Id_user)
        {
            InitializeComponent();
            this.Id_user = Convert.ToInt32(Id_user);

            foreach (var catg in entities.CATEGORY)
            {
                cbFiltr.Items.Add(catg);
            }
            foreach (var assort in entities.ASSORTIMENT)
            {
                LbAssort.Items.Add(assort);
            }
        }
        private void LbAssort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = LbAssort.SelectedItem as ASSORTIMENT;
            if (selection != null && !string.IsNullOrEmpty(selection.image))
            {
                try
                {
                    BitmapImage myBitmapImage = new BitmapImage();
                    myBitmapImage.BeginInit();
                    myBitmapImage.UriSource = new Uri(projectDirectory + @"\Photo\" + selection.image);
                    myBitmapImage.DecodePixelWidth = 200;
                    myBitmapImage.EndInit();
                    IImage.Source = myBitmapImage;

                    cbFiltr.SelectedIndex = selection.Id_category - 1;
                    cbFiltr.SelectedItem = selection.CATEGORY;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                    IImage.Source = null;
                    cbFiltr.SelectedIndex = -1;
                    LbAssort.SelectedIndex = -1;
                }
            }
            else
            {
                cbFiltr.SelectedIndex = -1;
                LbAssort.SelectedIndex = -1;
                IImage.Source = null;
            }
        }
        private void UpdateResultsCount(int totalCount)
        {
            resultsCountTextBlock.Text = $"Общее количество записей: {totalCount}";
        }
        private void UpdateSearchResultsCount(int count)
        {
            searchResultsCountTextBlock.Text = $"Количество записей в режиме поиска: {count}";
        }
        private void ShowNoResults()
        {
            noResultsTextBlock.Text = "Нет результатов поиска";
        }
        private void HideNoResults()
        {
            noResultsTextBlock.Text = string.Empty;
        }
        private void Poisk_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            LbAssort.Items.Clear();
            string text = Poisk.Text.ToLower();
            ObservableCollection<ASSORTIMENT> searchResults;

            if (string.IsNullOrEmpty(text))
            {
                searchResults = new ObservableCollection<ASSORTIMENT>(entities.ASSORTIMENT.ToList());
            }
            else
            {
                searchResults = new ObservableCollection<ASSORTIMENT>(entities.ASSORTIMENT.ToList()
                .Where(item => item.name_assorti.ToLower().Contains(text) || item.opisaniye_assorti.ToString().Contains(text))
                .ToList());
            }

            UpdateResultsCount(entities.ASSORTIMENT.Count());
            UpdateSearchResultsCount(searchResults.Count);

            foreach (var item in searchResults)
            {
                LbAssort.Items.Add(item);
            }

            if (searchResults.Count == 0)
            {
                ShowNoResults();
            }
            else
            {
                HideNoResults();
            }
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            РедактированиеАссортимента window = new РедактированиеАссортимента(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Главная window = new Главная(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }
        private void cbFiltr_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = cbFiltr.SelectedItem as CATEGORY;
            if (selectedCategory != null)
            {
                LbAssort.Items.Clear();
                var filteredItems = entities.ASSORTIMENT.Where(item 
                    => item.Id_category == selectedCategory.Id_category).ToList();
                foreach (var item in filteredItems)
                {
                    LbAssort.Items.Add(item);
                }
                UpdateResultsCount(filteredItems.Count);
                if (filteredItems.Count == 0)
                {
                    ShowNoResults();
                }
                else
                {
                    HideNoResults();
                }
            }
        }
    }
}












