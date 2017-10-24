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
using MaterialDesignThemes.Wpf;
using Client.DataAccess.Context;
using Client.Desktop.View;
using UserPage = Client.Desktop.View.UserPage;

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
            if (Properties.App.Default.FirstRun)
            {
                Properties.App.Default.LastUSN = 0;
                Properties.App.Default.FirstRun = false;
                Properties.App.Default.Save();
            }
            ConfigureDatabase();
        }

        private void Switch_MainPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new MainPage());
        }

        private void Switch_SpendingPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new SpendingPage());
        }

        private void Switch_CashAccountPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new CashAccountPage());
        }

        private void Switch_UserPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new UserPage());
        }

        private void ConfigureDatabase()
        {
            DbContextFactory.SetPassword(Properties.Login.Default.AccountId);
            Seed.SeedMethod();
        }
    }
}
