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
    public partial class ExpensesPage : Page
    {
        //TODO: Dialog windows

        //bool isExpense;
        //bool isAdd;
        //string selectedRowCashFlowId;

        public ExpensesPage()
        {
            InitializeComponent();

            this.DataContext = new ExpensesPageViewModel();

        //    btnExpense.Background = Brushes.Red;
        //    btnExpense.BorderBrush = Brushes.Red;
        //    isExpense = true;

        //    //Инициализация comboBox значениями из БД
        //    CashAccountRepository cashAccountRepository = new CashAccountRepository();
        //    cbCashAccount.DisplayMemberPath = "Name";
        //    cbCashAccount.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = cashAccountRepository.GetAllCashAccounts() });

        //    //Инициализация comboBox значениями из БД
        //    CategoryRepository categoryRepository = new CategoryRepository();
        //    cbCategory.DisplayMemberPath = "Name";
        //    cbCategory.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = categoryRepository.GetAllCategories() });

        //    //Загрузка данных в Grid в новом потоке
        //    Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        }

        //private void btnExpense_Click(object sender, RoutedEventArgs e)
        //{
        //    btnExpense.Background = Brushes.Red;
        //    btnExpense.BorderBrush = Brushes.Red;
        //    btnIncome.Background = Brushes.Gray;
        //    btnIncome.BorderBrush = Brushes.Gray;
        //    isExpense = true;
        //}

        //private void btnIncome_Click(object sender, RoutedEventArgs e)
        //{
        //    //TODO:Ask Ihor how to do it right, Discuss advisability of one interface for expense and income
        //    btnIncome.Background = Brushes.Green;
        //    btnIncome.BorderBrush = Brushes.Green;
        //    btnExpense.Background = Brushes.Gray;
        //    btnExpense.BorderBrush = Brushes.Gray;
        //    isExpense = false;
        //}

        //private void LoadData()
        //{
        //    ExpensesRepository expensesRepository = new ExpensesRepository();
        //    //TODO without id
        //    dataGrid.ItemsSource = expensesRepository.GetAllExpenses();
        //}

        //private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    if (Equals(eventArgs.Parameter, true) && isAdd)
        //    {
        //        ExpensesRepository expensesRepository = new ExpensesRepository();
        //        var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
        //        var categorySelectedItem = cbCategory.SelectedItem as Category;
        //        //TODO: Fix DateTime output format
        //        //TODO: Add check if all values are inputed 
        //        expensesRepository.AddExpense(cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), categorySelectedItem.Id, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
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
        //        ExpensesRepository expensesRepository = new ExpensesRepository();
        //        var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
        //        var categorySelectedItem = cbCategory.SelectedItem as Category;

        //        expensesRepository.UpdateExpense(selectedRowCashFlowId, cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), categorySelectedItem.Id, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
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

        //    Expense selectedExpense = (Expense)dataGrid.SelectedItem;
        //    selectedRowCashFlowId = selectedExpense.Id;
        //    cbCashAccount.Text = selectedExpense.Cash;
        //    tbAmount.Text = selectedExpense.Amount.ToString();
        //    tbDescription.Text = selectedExpense.Description;
        //    cbCategory.Text = selectedExpense.Category;
        //    dpDate.Text = selectedExpense.Date.Date.ToString();
        //}

        //private void btnDeleteTransaction_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        //{
        //    isAdd = true;
        //}

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnAccept_Click(object sender, RoutedEventArgs e)
        //{
        //    ExpensesRepository expensesRepository = new ExpensesRepository();
        //    var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
        //    var categorySelectedItem = cbCategory.SelectedItem as Category;
        //    //TODO: Fix DateTime output format
        //    //TODO: Add check if all values are inputed 
        //    expensesRepository.AddExpense(cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), categorySelectedItem.Id, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
        //    Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        //    cbCashAccount.Text = "";
        //    tbAmount.Text = "";
        //    tbDescription.Text = "";
        //    cbCategory.Text = "";
        //    dpDate.Text = "";
        //}
    }
}

