using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            Users Users = new Users();
            Users.UserName = tbLogin.Text;
            Users.Password = tbPassword.Text;
            Users.FullName = tbFullName.Text;
            Users.PhoneNumber = tbPhone.Text;
            

            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {

               

                if (bazaDanychOkazjaEntities.Users.ToList().Any(x => x.UserName == Users.UserName))
                {
                    MessageBox.Show("Choose other UserName.");
                    return;
                }

                
                bazaDanychOkazjaEntities.Users.Add(Users);
                bazaDanychOkazjaEntities.SaveChanges();
                MessageBox.Show("Done.");
            }
        }

        private void btnPokazBazeDanych_Click(object sender, RoutedEventArgs e)
        {
            ShowLogins showLogins = new ShowLogins();
            showLogins.Show();
        }
    }
}
