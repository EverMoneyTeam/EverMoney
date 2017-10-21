using System.Windows.Controls;
using Client.DataAccess.Repository;
using System.ComponentModel;
using System.Threading;
using System;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using Client.Desktop.ViewModel;

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class CashFlowPage : Page
    {
        //TODO: Dialog windows

        //bool isCashFlow;
        //bool isAdd;
        //string selectedRowCashFlowId;

        public CashFlowPage()
        {
            InitializeComponent();

            this.DataContext = new CashFlowPageViewModel();
        }
    }
}

