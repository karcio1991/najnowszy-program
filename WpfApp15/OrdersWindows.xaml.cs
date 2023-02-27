using finalny_program_managementSystem.Model;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Objects;
using System.Linq;
using System.Management.Instrumentation;
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

            

            SetAllCustomers();
            DisplayDataGridCustomers();
            DisplayDataGridProducts();
            DisplayDataGridOrders();
            DisplayComboBox();



        }


        public void DisplayComboBox()
        {
            List<string> ListToComboBox = new List<string>();
            ListToComboBox.Add("");

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
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

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                foreach (var item in bazaDanychOkazjaEntities.Products)
                {
                    ListOfProducts.Add(new Products { Id = item.Id, CategoriesName = item.CategoriesName, ProductName = item.ProductName, Description = item.Description, Price = item.Price, NumberOfProducts = item.NumberOfProducts });
                }
                gridProductsList.ItemsSource = ListOfProducts;
            }


            CollectionView widok = (CollectionView)CollectionViewSource.GetDefaultView(gridProductsList.ItemsSource);
            widok.SortDescriptions.Add(new System.ComponentModel.SortDescription("ProductName", ListSortDirection.Ascending));

            widok.Filter = FiltrUser;


        }

        public void DisplayDataGridOrders()
        {
            ListOfOrders.Clear();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Orders)
                {

                    //problem jak ustawic productName dla listy,cala lista jest tego samego typu
                    ListOfOrders.Add(new Orders()
                    {
                        Customers = item.Customers,
                        Quantity = item.Quantity,
                        TotPrice = item.TotPrice,
                        UPrice = item.UPrice,
                        Users = item.Users,
                        //ProductName  = item.ProductName,
                        Products = item.Products,
                        Id = item.Id,

                    });
                }
                gridOrdersList.ItemsSource = ListOfOrders;
            }

        }




        private void btnInsertOrder_Click(object sender, RoutedEventArgs e)
        {

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                Customers cust = gridCustomersList.SelectedItem as Customers;
                int indexOfCustomerSystem = bazaDanychOkazjaEntities.Customers.Single(x => x.CustomerName == cust.CustomerName).Id;



                Products prod = gridProductsList.SelectedItem as Products;
                cust.LastOrder = DateTime.Now;


                List<Products> prodList = new List<Products>();

                for (int i = 0; i < prod.NumberOfProducts; i++)
                {
                    prodList.Add(prod);
                }

                int numberOfOrders = bazaDanychOkazjaEntities.Orders.Count();

                Orders orders = new Orders()
                {
                    Customers = bazaDanychOkazjaEntities.Customers.Single(x => x.CustomerName == cust.CustomerName),
                    Quantity = prod.NumberOfProducts,
                    Products = prodList,
                    UPrice = prod.Price,
                    TotPrice = prod.NumberOfProducts * prod.Price,
                    Users = bazaDanychOkazjaEntities.Users.Single(x => x.Online == true),
                    Id = numberOfOrders++,
                };

                orders.Customers.LastOrder = DateTime.Now;


                // to jest dla OrdersCount
                int numberOfOrderForEachCustomer = bazaDanychOkazjaEntities.Orders.Where(x => x.Customers.CustomerName == cust.CustomerName).Count();

                orders.Customers.OrdersCount = numberOfOrderForEachCustomer;

                //to jest dla OrderAmount
                decimal ordersAmount = 0;

                List<Orders> listOfOrdersToCountAmount = bazaDanychOkazjaEntities.Orders.Where(x => x.Customers.CustomerName == cust.CustomerName).ToList();

                foreach (var item in listOfOrdersToCountAmount)
                {
                    ordersAmount += item.TotPrice;
                }


                orders.Customers.OrdersAmountValue = Convert.ToInt32(ordersAmount);



                bazaDanychOkazjaEntities.Orders.Add(orders);
                bazaDanychOkazjaEntities.SaveChanges();

            }


            DisplayDataGridOrders();
            DisplayDataGridCustomers();
            SetAllCustomers();
        }


        public void CalculateMethodCustomers(Customers custom)
        {



            Customers selectedCustomers = null;


            if (custom == null)
                selectedCustomers = gridCustomersList.SelectedItem as Customers;
            else
                selectedCustomers = custom;


            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                int numberOfOrderForEachCustomer = bazaDanychOkazjaEntities.Orders.Where(x => x.Customers.CustomerName == selectedCustomers.CustomerName).Count();


                bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomers.Id).First().OrdersCount = numberOfOrderForEachCustomer;


                decimal ordersAmount = 0;

                List<Orders> listOfOrdersToCountAmount = bazaDanychOkazjaEntities.Orders.Where(x => x.Customers.CustomerName == selectedCustomers.CustomerName).ToList();

                foreach (var item in listOfOrdersToCountAmount)
                {
                    ordersAmount += item.TotPrice;
                }

                bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomers.Id).First().OrdersAmountValue =
            Convert.ToInt32(ordersAmount);


                bazaDanychOkazjaEntities.SaveChanges();

            }

            DisplayDataGridCustomers();


        }



        private void btnViewOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbWyborCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnClearAllgrid_Click(object sender, RoutedEventArgs e)
        {
            
            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                List<Orders> listaOfOrders = bazaDanychOkazjaEntities.Orders.ToList();


                foreach (var item in listaOfOrders)
                {
                    bazaDanychOkazjaEntities.Orders.Remove(item);
                }
                bazaDanychOkazjaEntities.SaveChanges();

                MessageBox.Show(bazaDanychOkazjaEntities.Orders.Count().ToString());

                SetAllCustomers();
                DisplayDataGridCustomers();
                DisplayDataGridOrders();

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Orders selectedItemOrder = gridOrdersList.SelectedItem as Orders;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                MessageBox.Show(selectedItemOrder.Id.ToString());
                Orders order2 = bazaDanychOkazjaEntities.Orders.Where(x => x.Id == selectedItemOrder.Id).First();


               

                bazaDanychOkazjaEntities.Orders.Remove(order2);
                bazaDanychOkazjaEntities.SaveChanges();

               
            }
            SetAllCustomers();
            DisplayDataGridOrders();
            

        }



        private void gridCustomersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void tbSearchForProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(gridProductsList.ItemsSource).Refresh();
        }


        public bool FiltrUser(object item)
        {

            if (String.IsNullOrEmpty(tbSearchForProduct.Text))
            {
                return true;
            }
            else
            {
                return ((item as Products).ProductName.IndexOf(tbSearchForProduct.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }


        }

        public void SetAllCustomers()
        {
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Customers)
                {
                    CalculateMethodCustomers(item);
                }
            }

        }
    }
}
