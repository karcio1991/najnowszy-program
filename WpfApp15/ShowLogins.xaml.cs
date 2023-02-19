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
    /// Logika interakcji dla klasy ShowLogins.xaml
    /// </summary>
    public partial class ShowLogins : Window
    {
        public ShowLogins()
        {
            InitializeComponent();
            DisplayTable();
        }

        ObservableCollection<Users> ListOfUsers = new ObservableCollection<Users>();
        private void DisplayTable()
        {
            ListOfUsers.Clear();

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                var Users = bazaDanychOkazjaEntities.Users;
                foreach (var item in Users)
                {
                    ListOfUsers.Add(item);
                }
                lstLoginy.ItemsSource = ListOfUsers;
            }
        }
    }
}
