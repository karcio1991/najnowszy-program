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

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Categories categories = new Categories();
            categories.CategoryName = tbNewCategory.Text;
            categories.Products = new Products();
            categories.Products.Customers = new Customers();
            categories.Products.Orders = new Orders();
            categories.Products.Orders.Users = new Users();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                bazaDanychOkazjaEntities.Categories.Add(categories);
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
                    ListOfCategory.Add(item);
                }
                gridCategoriesList.ItemsSource = ListOfCategory;
            }
        }
    }
}
