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
    /// Logika interakcji dla klasy OrdersWindows.xaml
    /// </summary>
    public partial class OrdersWindows : Window
    {

        ObservableCollection<Customers> ListOfCustomers = new ObservableCollection<Customers>();
        ObservableCollection<Products> ListOfProducts = new ObservableCollection<Products>();
        ObservableCollection<Orders> ListOfOrders = new ObservableCollection<Orders>();

        public OrdersWindows()
        {
            InitializeComponent();
            DisplayDataGridCustomers();
            DisplayDataGridProducts();
            DisplayDataGridOrders();
            DisplayComboBox();
        }


        public void DisplayComboBox()
        {
            List<string> ListToComboBox = new List<string>();
            ListToComboBox.Add("");

            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Categories)
                {
                    ListToComboBox.Add(item.CategoryName);
                }
                cbWyborCategories.ItemsSource = ListToComboBox;
            }

           
        }


        public void DisplayDataGridCustomers()
        {
            ListOfCustomers.Clear();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Customers)
                {
                    ListOfCustomers.Add(item);
                }
                gridCustomersList.ItemsSource = ListOfCustomers;
            }


            
        }

        public void DisplayDataGridProducts()
        {
            ListOfProducts.Clear();

            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    ListOfProducts.Add(item);
                }
                gridProductsList.ItemsSource = ListOfProducts;
            }


        }

        public void DisplayDataGridOrders()
        {
            ListOfOrders.Clear();

            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Orders)
                {
                    ListOfOrders.Add(item);
                }
                gridOrdersList.ItemsSource = ListOfOrders;
            }

        }


        private void btnInsertOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbWyborCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnClearAllgrid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gridCustomersList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void gridCustomersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
