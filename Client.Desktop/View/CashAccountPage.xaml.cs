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
using Client.Desktop.ViewModel;

namespace Client.Desktop.View
{
    /// <summary>
    /// Interaction logic for CashAccountPage.xaml
    /// </summary>
    public partial class CashAccountPage : Page
    {
        public CashAccountPage()
        {
            InitializeComponent();

            this.DataContext = new CashAccountViewModel();
        }
    }
}
