using Client.Desktop.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Client.DataAccess.Context;
using Client.DataAccess.Model;
using Client.DataAccess.Repository;

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string refreshToken;
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbxLogin.Text;
            string password = pbxPassword.Password;

            var responseData = await ApiAuthService.PostAsync(ApiRequestEnum.Login, new { login, password });
            if (!responseData.IsSuccessStatusCode)
            {
                MessageBoxExtension.ShowError(responseData);
                return;
            }

            var responseJwtToken = await responseData.Content.ReadAsAsync<ResponseJWTFormat>();
            refreshToken = responseJwtToken.RefreshToken;
            Properties.Login.Default.JwtToken = responseJwtToken.AccessToken;
            Properties.Login.Default.UserLogin = login;
            Properties.Login.Default.AccountId = new JwtSecurityToken(responseJwtToken.AccessToken).Subject;
            Properties.Login.Default.ExpiresIn = responseJwtToken.ExpiresIn;
            Properties.Login.Default.Save();
            AddNewUser();
            //await DialogHostExtension.ShowInfo("Ok");

            NavigationService.Navigate(new MainPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddNewUser()
        {
            var accountId = Properties.Login.Default.AccountId;
            DbContextFactory.SetPassword(accountId);
            var accounts = AccountRepository.GetAllAccounts();
            if (accounts.All(a => a.Id != accountId))
            {
                AccountRepository.AddAccount(
                    new Account
                    {
                        Id = accountId,
                        Login = Properties.Login.Default.UserLogin
                    });
                var defaultAccount = accounts.FirstOrDefault(a => a.Id == "00000000-0000-0000-0000-000000000000");
                if (defaultAccount != null && defaultAccount.IsCurrent)
                {
                    AccountRepository.UpdateDefaultAccount(accountId);
                }
            }

            AccountRepository.SetLoginAccount(accountId, refreshToken);
        }
    }
}
