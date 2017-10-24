using Client.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Client.Desktop.Helper;
using Client.Desktop.Utils;
using MaterialDesignThemes.Wpf;

namespace Client.Desktop.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void btnReg_Click(object sender, RoutedEventArgs e)
        {
            var login = tbxLogin.Text;
            var password = pbxPassword.Password;

            try
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.Registration, new { login, password });
                if (response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowInfo("Registration successful");
                }
                else
                {
                    MessageBoxExtension.ShowError(response);
                }
            }
            catch (HttpRequestException exception)
            {
                MessageBoxExtension.ShowError(exception);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
