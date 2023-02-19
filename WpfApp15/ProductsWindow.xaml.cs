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
    /// Logika interakcji dla klasy ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        ObservableCollection<Products> ListOfProducts = new ObservableCollection<Products>();

        public ProductsWindow()
        {
            InitializeComponent();
            DisplayDataGrid();
            DisplayComboBox();
        }

        public void DisplayDataGrid()
        {
            ListOfProducts.Clear();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    ListOfProducts.Add(item);
                }
                gridProductsList.ItemsSource = ListOfProducts;
            }

        }

        public void DisplayComboBox()
        {
            List<string> listOfComboBox = new List<string>();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Categories)
                {
                    listOfComboBox.Add(item.CategoryName);
                }
                cbCategories.ItemsSource = listOfComboBox;
            }
        }

        private void SliderNumberOfProducts_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void gridProductsList_Selected(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbProductName.Text))
            {
                MessageBox.Show("Write a ProductName.");
                return;
            }

            Products products = new Products();
            products.Description = tbProductDesc.Text;
            products.ProductName = tbProductName.Text;
            products.Price = decimal.Parse(tbProductPrice.Text);

            //poki co: 
            products.Customers = null;
            products.Categories = new Categories();
            products.Orders = new Orders();
            products.Orders.Users = new Users();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                //bez kategorii na razie.
                /*
                if (cbCategories.SelectedItem != null)
                {
                    var findCategories = bazaDanychOkazjaEntities.Categories.FirstOrDefault(x => x.CategoryName == cbCategories.Text);

                    if (findCategories != null)
                        products.Categories = findCategories;
                    else
                        products.Categories = null;
                }*/

                products.NumberOfProducts = (int)SliderNumberOfProducts.Value;
                bazaDanychOkazjaEntities.Products.Add(products);
                bazaDanychOkazjaEntities.SaveChanges();
            }

            DisplayDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbProductDesc.Text = "";
            tbProductName.Text = "";
            tbProductPrice.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
