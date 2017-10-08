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
                Seed.SeedMethod();
                Properties.App.Default.FirstRun = false;
                Properties.App.Default.Save();
            }
            Grid.Children.Add(new DialogHost() { Name = "MainDialog" });
            Frame.NavigationService.Navigate(new MainPage());
        }

        private void Switch_MainPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new MainPage());
        }


        private void Switch_ExpensesPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new ExpensesPage());
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        private void Switch_UserPage(object sender, MouseButtonEventArgs e)
        {
            Frame.NavigationService.Navigate(new UserPage());
        }
    }
}
