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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            if (Properties.Login.Default.JwtToken != "")
            {
                btnLogInLogOut.Content = "Выход";
                btnLogInLogOut.Click += new RoutedEventHandler(ButtonLogOut);

                btnRegister.Visibility = Visibility.Hidden;

                tbGreetings.Text = "Добро Пожаловать, " + Properties.Login.Default.UserLogin;
            }
        }

        private void ButtonLogOut(object sender, RoutedEventArgs e)
        {
            Properties.Login.Default.JwtToken = "";
            Properties.Login.Default.UserLogin = "Guest";
            Properties.Login.Default.UserPassword = "";
            Properties.Login.Default.Save();

            NavigationService.Navigate(new LoginPage());
        }

        private void ButtonLogIn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void ButtonRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }


    }
}
