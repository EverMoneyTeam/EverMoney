using System;
using System.Windows;
using System.Windows.Controls;
using Client.DataAccess.Repository;
using Client.Desktop.Models;
using Client.Desktop.Pages;
using Client.Desktop.Sync;
using Client.Desktop.Utils;

namespace Client.Desktop.View
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
            }

            GreetingsMessage.Text = "Welcome, " + Properties.Login.Default.UserLogin;
        }

        private async void ButtonLogOut(object sender, RoutedEventArgs e)
        {
            string accountId = JWTParser.ReturnAccountId(Properties.Login.Default.JwtToken);
            string refreshToken = AccountRepository.GetAccount(Properties.Login.Default.AccountId).RefreshToken;
            var responseData = await ApiAuthService.PostAsync(ApiRequestEnum.Logout, new { refreshToken, accountId });
            if (responseData != null && !responseData.IsSuccessStatusCode)
                MessageBoxExtension.ShowError(responseData);

            Properties.Login.Default.JwtToken = "";
            Properties.Login.Default.UserLogin = "Guest";
            Properties.Login.Default.ExpiresIn = DateTime.MinValue;
            Properties.Login.Default.Save();

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
            var isTokenUpdated = await SyncProvider.UpdateToken();

            if (!isTokenUpdated)
            {
                MessageBoxExtension.ShowError("Error while token refresh");
                return;
            }

            var lastClientUsn = Properties.App.Default.LastUSN;
            var lastServerUsn = await SyncProvider.SyncStateCheck();

            if (lastClientUsn < lastServerUsn)
            {
                await SyncProvider.IncrementalSync();
            }

            await SyncProvider.SendChanges();
        }
    }
}
