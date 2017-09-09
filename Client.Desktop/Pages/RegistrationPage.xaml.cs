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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            string login = tbxLogin.Text;
            string password = pbxPassword.Password;

            Authorization authorization = new Authorization();
            ResponseData response = authorization.RegisterAccount(login, password);
            if (response == null)
            {
                MessageBox.Show("Registration failed");
            }
            else if (response.Message == "Ok")
            {
                MessageBox.Show("Registration successful");
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
