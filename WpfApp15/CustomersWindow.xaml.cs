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

        }
    }
}
