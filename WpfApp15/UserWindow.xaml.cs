using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();


  
            
            DisplayUsers();


        }



    
        ObservableCollection<Users> ListOfUsers = new ObservableCollection<Users>();

        public void DisplayUsers()
        {
            ListOfUsers.Clear();
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Users)
                {
                    ListOfUsers.Add(item);

                }
                gridUserList.ItemsSource = ListOfUsers;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFullName.Text = "";
            tbPassword.Text = "";
            tbPhone.Text = "";
            tbUsersName.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = gridUserList.SelectedItem as Users;

            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                foreach (var item in bazaDanychOkazjaEntities.Users.Where(x => x.Id == selectedUser.Id))
                {

                    item.UserName = selectedUser.UserName;
                    item.PhoneNumber = selectedUser.PhoneNumber;
                    item.FullName = selectedUser.FullName;
                    item.Password = selectedUser.Password;


                }
                bazaDanychOkazjaEntities.SaveChanges();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            Users user = gridUserList.SelectedItem as Users;
            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                Users user1 = bazaDanychOkazjaEntities.Users.ToList().Where(x => x.Id == user.Id).First();
                bazaDanychOkazjaEntities.Users.Remove(user1);
                bazaDanychOkazjaEntities.SaveChanges();
            }
            DisplayUsers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            Users users = new Users()
            {
                UserName = tbUsersName.Text,
                FullName = tbFullName.Text,
                PhoneNumber = tbPhone.Text,
                Password = tbPassword.Text,
                Orders = null,

            };



            using (BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                //sprawdz czy juz jest taki user
                bool checkIfExists = bazaDanychOkazjaEntities.Users.ToList().Any(x => x.UserName == users.UserName);
                if (checkIfExists)
                {
                    MessageBox.Show("This User already exists. Please write other UserName");
                    return;
                }

                bazaDanychOkazjaEntities.Users.Add(users);
                bazaDanychOkazjaEntities.SaveChanges();

            }

            DisplayUsers();

        }


    }
}
