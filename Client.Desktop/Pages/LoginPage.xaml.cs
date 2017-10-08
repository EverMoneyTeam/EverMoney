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
using Client.Desktop.Utils;
using MaterialDesignThemes.Wpf;
using System.Net.Http;

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

            var responseData = await ApiAuthService.PostAsync(ApiRequestEnum.Login, new {login, password});
            if (!responseData.IsSuccessStatusCode)
            {
                MessageBoxExtension.ShowError(responseData);
                return;
            }

            var responseJwtToken = await responseData.Content.ReadAsAsync<ResponseJWTFormat>();
            Properties.Login.Default.JwtToken = responseJwtToken.AccessToken;
            Properties.Login.Default.UserLogin = login;
            Properties.Login.Default.RefreshToken = responseJwtToken.RefreshToken;
            Properties.Login.Default.Save();
            //await DialogHostExtension.ShowInfo("Ok");

            NavigationService.Navigate(new MainPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
