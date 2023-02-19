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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp15
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using(BazaDanychOkazjaEntities2 bazaDanychOkazjaEntities = new BazaDanychOkazjaEntities2())
            {
                var users = bazaDanychOkazjaEntities.Users;
                var correctLogin = users.FirstOrDefault(us => us.UserName == tbUsersName.Text && us.Password == tbPassword.Password);
                if(correctLogin != null)
                {
                    this.Hide();
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                }
                else
                {
                    MessageBox.Show("Wrong data.");
                }
            }
        }
    }
}
