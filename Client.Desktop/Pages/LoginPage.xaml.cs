using Client.Desktop.ApiOperations;
using Client.Desktop.Models;
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

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbxLogin.Text;
            string password = pbxPassword.Password;

            //string login = "login";
            //string password = BCrypt.Net.BCrypt.HashPassword("password");

            ResponseJWTFormat responseData = await Authorization.GetTokenAsync(login, password);
            if (responseData == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }

            Properties.Login.Default.JwtToken = responseData.AccessToken;
            Properties.Login.Default.UserLogin = login;
            Properties.Login.Default.UserPassword = password;
            Properties.Login.Default.Save();

            NavigationService.Navigate(new Welcome());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
