using System.Windows;
using System.Windows.Controls;
using Client.Desktop.ViewModel;

namespace Client.Desktop.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class SpendingPage : Page
    {
        public SpendingPage()
        {
            InitializeComponent();

            this.DataContext = new SpendingPageViewModel();
        }


        public SpendingPageViewModel ViewModel => DataContext as SpendingPageViewModel;

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (ViewModel == null) return;

            ViewModel.SelectedCategory = e.NewValue;
        }
    }
}

