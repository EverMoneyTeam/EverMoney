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
using Client.Desktop.Models;
using Client.Desktop.Utils;

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            if (Properties.Login.Default.JwtToken != "")
            {
                GuestGrid.Visibility = Visibility.Hidden;
                UserGrid.Visibility = Visibility.Visible;

                GreetingsMessage.Text = "Добро Пожаловать, " + Properties.Login.Default.UserLogin;
            }
        }

        private async void ButtonLogOut(object sender, RoutedEventArgs e)
        {
            string accountId = JWTParser.ReturnAccountId(Properties.Login.Default.JwtToken);
            string refreshToken = Properties.Login.Default.RefreshToken;
            var responseData = await ApiAuthService.PostAsync(ApiRequestEnum.Logout, new { refreshToken, accountId });
            if (responseData != null && !responseData.IsSuccessStatusCode)
                MessageBoxExtension.ShowError(responseData);

            Properties.Login.Default.JwtToken = "";
            Properties.Login.Default.UserLogin = "Guest";
            Properties.Login.Default.UserPassword = "";
            Properties.Login.Default.RefreshToken = "";
            Properties.Login.Default.Save();

            //NavigationService.Refresh();
            NavigationService.Navigate(new MainPage());
        }

        private void ButtonLogIn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void ButtonRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private async void ButtonSync(object sender, RoutedEventArgs e)
        {
            var response = await ApiAuthService.GetAsync(ApiRequestEnum.Health);
            if (response != null && !response.IsSuccessStatusCode)
                MessageBoxExtension.ShowError(response);
        }
    }
}
