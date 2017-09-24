using Client.Desktop.Pages;
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

namespace Client.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var token = Properties.Login.Default.JwtToken;
            if (string.IsNullOrWhiteSpace(token))
                Frame.NavigationService.Navigate(new LoginPage());
            else
                Frame.NavigationService.Navigate(new Welcome());
        }

        private void Switch_MainPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new MainPage());
        }
    }
}
