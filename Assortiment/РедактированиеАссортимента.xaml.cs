using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Assortiment
{
    /// <summary>
    /// Логика взаимодействия для РедактированиеАссортимента.xaml
    /// </summary>
    public partial class РедактированиеАссортимента : Window
    {
        Entities entities = new Entities();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public int Id_user { get; set; }
        public РедактированиеАссортимента(string Id_user)
        {
            InitializeComponent();

            this.Id_user = Convert.ToInt32(Id_user);

            foreach (var ass in entities.ASSORTIMENT)
            {
                LbAssorti.Items.Add(ass);
            }
            foreach (var catg in entities.CATEGORY)
            {
                cbCateg.Items.Add(catg);
            }
        }

private void LbAssorti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = LbAssorti.SelectedItem as ASSORTIMENT;
            if (selection != null)
            {
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(projectDirectory + "\\Photo\\" + selection.image);
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();
                IImage.Source = myBitmapImage;

                tbNameAss.Text = selection.name_assorti.ToString();
                tbOpis.Text = selection.opisaniye_assorti.ToString();
                tbPrice.Text = selection.price.ToString();
                cbCateg.SelectedIndex = selection.Id_assorti - 1;
                cbCateg.SelectedItem = selection.CATEGORY;
            }
            else
            {
                cbCateg.SelectedIndex = -1;
                LbAssorti.SelectedIndex = -1;
                tbNameAss.Text = "";
                tbOpis.Text = "";
                tbPrice.Text = "";

            }
        }
        private void bDobPhoto_Click(object sender, RoutedEventArgs e)
        {
            var selected_item = LbAssorti.SelectedItem as ASSORTIMENT;
            OpenFileDialog mer = new OpenFileDialog();

            mer.InitialDirectory = projectDirectory + "\\Photo\\";

            if (mer.ShowDialog() == true && !string.IsNullOrWhiteSpace(mer.FileName))
                IImage.Source = new BitmapImage(new Uri(mer.FileName));
            try
            {
                File.Copy(mer.FileName, mer.InitialDirectory + mer.SafeFileName);
                MessageBox.Show("Ошибка");
            }
            catch
            {

            }
        }
        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var item = LbAssorti.SelectedItem as ASSORTIMENT;
            if (
                tbNameAss.Text == "" ||
                tbOpis.Text == "" ||
                tbPrice.Text == "" ||
                IImage.Source == null ||
                cbCateg.SelectedIndex == -1 
                )
            {
                MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                if (item == null)
                {
                    item = new ASSORTIMENT();
                    entities.ASSORTIMENT.Add(item);
                    LbAssorti.Items.Add(item);
                }
                string fullFileName = IImage.Source.ToString();
                fullFileName = fullFileName.Replace(@"file:///", "");
                FileInfo fileInfo = new FileInfo(fullFileName);
                item.image = fileInfo.Name;
                try
                {
                    item.name_assorti = tbNameAss.Text;
                    item.opisaniye_assorti = tbOpis.Text;
                    item.price = int.Parse(tbPrice.Text);
                    item.Id_category = (cbCateg.SelectedItem as CATEGORY).Id_category;

                }
                catch
                {
                    MessageBox.Show("Некорректные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                entities.SaveChanges();
                LbAssorti.Items.Refresh();
                MessageBox.Show("Готово", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Ассортимент window = new Ассортимент(Id_user.ToString());
            this.Close();
            window.ShowDialog();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var resalt = MessageBox.Show("Точно удалить?", "Удаление", MessageBoxButton.YesNo);
            if (resalt == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var deletedItem = LbAssorti.SelectedItem as ASSORTIMENT;
                if (deletedItem != null)
                {

                    try
                    {
                        entities.ASSORTIMENT.Remove(deletedItem);
                        entities.SaveChanges();
                        LbAssorti.Items.Remove(deletedItem);
                        LbAssorti.Items.Refresh();

                        IImage.Source = null;
                        tbNameAss.Text = "";
                        tbOpis.Text = "";
                        tbPrice.Text = "";
                        tbNameAss.Focus();
                        LbAssorti.SelectedIndex = -1;
                        cbCateg.SelectedIndex = -1;


                        MessageBox.Show("Удаление прошло успешно", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Эти данные используют другие таблицы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                }
            }
        }
        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            IImage.Source = new BitmapImage();
            tbNameAss.Text = "";
            tbOpis.Text = "";
            tbPrice.Text = "";
            tbNameAss.Focus();
            LbAssorti.SelectedIndex = -1;
            cbCateg.SelectedIndex = -1;
        }
    }
    
}

