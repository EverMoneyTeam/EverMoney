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
    public partial class CashFlowsPage : Page
    {
        //TODO: Dialog windows

        //bool isCashFlow;
        //bool isAdd;
        //string selectedRowCashFlowId;

        public CashFlowsPage()
        {
            InitializeComponent();

            this.DataContext = new CashFlowsPageViewModel();
        }


        //private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    if (Equals(eventArgs.Parameter, true) && isAdd)
        //    {
        //        CashFlowsRepository CashFlowsRepository = new CashFlowsRepository();
        //        var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
        //        var categorySelectedItem = cbCategory.SelectedItem as Category;
        //        //TODO: Fix DateTime output format
        //        //TODO: Add check if all values are inputed 
        //        CashFlowsRepository.AddCashFlow(cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), categorySelectedItem.Id, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
        //        Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        //        cbCashAccount.Text = "";
        //        tbAmount.Text = "";
        //        tbDescription.Text = "";
        //        cbCategory.Text = "";
        //        dpDate.Text = "";
        //    }
        //    //TODO:Ask Ihor how to do it right
        //    else if (Equals(eventArgs.Parameter, true) && !isAdd)
        //    {
        //        CashFlowsRepository CashFlowsRepository = new CashFlowsRepository();
        //        var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
        //        var categorySelectedItem = cbCategory.SelectedItem as Category;

        //        CashFlowsRepository.UpdateCashFlow(selectedRowCashFlowId, cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), categorySelectedItem.Id, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
        //        Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        //        cbCashAccount.Text = "";
        //        tbAmount.Text = "";
        //        tbDescription.Text = "";
        //        cbCategory.Text = "";
        //        dpDate.Text = "";
        //    }
        //}

        //private void btnUpdateTransaction_Click(object sender, RoutedEventArgs e)
        //{
        //    //TODO: Add check that row is selected
        //    isAdd = false;

        //    CashFlow selectedCashFlow = (CashFlow)dataGrid.SelectedItem;
        //    selectedRowCashFlowId = selectedCashFlow.Id;
        //    cbCashAccount.Text = selectedCashFlow.Cash;
        //    tbAmount.Text = selectedCashFlow.Amount.ToString();
        //    tbDescription.Text = selectedCashFlow.Description;
        //    cbCategory.Text = selectedCashFlow.Category;
        //    dpDate.Text = selectedCashFlow.Date.Date.ToString();
        //}

    }
}

