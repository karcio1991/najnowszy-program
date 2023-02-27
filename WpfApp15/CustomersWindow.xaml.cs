using Microsoft.EntityFrameworkCore.Query.Internal;
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
    /// Logika interakcji dla klasy CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        public CustomersWindow()
        {
            InitializeComponent();
            DisplayDataGridCustomers();
        }

        ObservableCollection<Customers> ListOfCustomers = new ObservableCollection<Customers>();

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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbCustomerName.Text == "CustomerName" || tbCustomerPhone.Text == "CustomerPhone" || tbCustomerName.Text == "" || tbCustomerPhone.Text == "")
            {
                MessageBox.Show("Input CustomerName and Phone.");
                return;
            }

            Customers customers = new Customers();
            customers.CustomerName = tbCustomerName.Text;
            customers.CustomerPhone = tbCustomerPhone.Text;
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                bazaDanychOkazjaEntities.Customers.Add(customers);
                bazaDanychOkazjaEntities.SaveChanges();
            }
            DisplayDataGridCustomers();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Customers cust1 = gridCustomersList.SelectedItem as Customers;
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                Customers cust2 = bazaDanychOkazjaEntities.Customers.ToList().Where(x => x.Id == cust1.Id).First();
                bazaDanychOkazjaEntities.Customers.Remove(cust2);
                bazaDanychOkazjaEntities.SaveChanges();
            }
            DisplayDataGridCustomers();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbCustomerId.Text = "";
            tbCustomerName.Text = "";
            tbCustomerPhone.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            var selectedCustomer = gridCustomersList.SelectedItem as Customers;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomer.Id))
                {

                    item.CustomerName = selectedCustomer.CustomerName;
                    item.CustomerPhone = selectedCustomer.CustomerPhone;

                    
                }
                bazaDanychOkazjaEntities.SaveChanges();
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = gridCustomersList.SelectedItem as Customers;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                Customers customer = bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomer.Id).First();


                if (customer.Orders == null)
                {
                    MessageBox.Show("This customer does not have orders.");
                    return;
                }
                else
                {
                    MessageBox.Show("This customer has " + customer.Orders.Count);
                }
            }
        }

        private void btnAmount_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = gridCustomersList.SelectedItem as Customers;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                Customers customer = bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomer.Id).First();


                if (customer.Orders == null)
                {
                    MessageBox.Show("This customer does not have orders.");
                    return;
                }
                else
                {

                    decimal wholePrice = 0;
                    foreach (var item in customer.Orders)
                    {
                        wholePrice += item.TotPrice;
                    }
                    MessageBox.Show("The total value of all orders is  " + wholePrice);
                }
            }
        }

        private void btnDate_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = gridCustomersList.SelectedItem as Customers;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

                Customers customer = bazaDanychOkazjaEntities.Customers.Where(x => x.Id == selectedCustomer.Id).First();


                if (customer.Orders == null)
                {
                    MessageBox.Show("This customer does not have orders.");
                    return;
                }
                else
                {

                    MessageBox.Show("The last order date is  " + customer.LastOrder);
                }
            }
        }
    }
}
