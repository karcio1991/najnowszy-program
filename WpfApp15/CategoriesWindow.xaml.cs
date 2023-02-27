using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp15
{
    /// <summary>
    /// Logika interakcji dla klasy CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        ObservableCollection<Categories> ListOfCategory = new ObservableCollection<Categories>();

        public CategoriesWindow()
        {
            InitializeComponent();
            DisplayCategories();
        }

        private void gridCategoriesList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbNewCategory.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                MessageBox.Show(bazaDanychOkazjaEntities.Categories.Count().ToString());
            }

        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            // NIE DODAJAMY SAMYCH KATEGORII JEDYNIE TUTAJ NIMI ZARZADZAMY, DODAJEMY JE WRAZ Z PRODUKTAMI
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                int index = bazaDanychOkazjaEntities.Categories.ToList().LastOrDefault().Id;
                Categories cata = new Categories()
                {
                    Id = index + 1,
                    CategoryName = tbNewCategory.Text,
                    Products = null,
                };
                bazaDanychOkazjaEntities.Categories.Add(cata);
                bazaDanychOkazjaEntities.SaveChanges();
            }

            DisplayCategories();

        }

        public void DisplayCategories()
        {
            ListOfCategory.Clear();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Categories)
                {
                    ListOfCategory.Add(new Categories() { CategoryName = item.CategoryName, Id = item.Id, Products = item.Products });
                }
                gridCategoriesList.ItemsSource = ListOfCategory;
            }
        }
    }
}
