using System.Windows.Controls;
using Client.DataAccess.Repository;
using System.ComponentModel;
using System.Threading;
using System;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class ExpensesPage : Page
    {
        bool isExpense;
        bool isAdd;
        public ExpensesPage()
        {
            InitializeComponent();

            btnExpense.Background = Brushes.Red;
            btnExpense.BorderBrush = Brushes.Red;
            isExpense = true;

            //Инициализация comboBox значениями из БД
            CashAccountRepository cashAccountRepository = new CashAccountRepository();
            cbCashAccount.DisplayMemberPath = "Name";
            cbCashAccount.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = cashAccountRepository.GetAllCashAccounts() });

            //Инициализация comboBox значениями из БД
            CategoryRepository categoryRepository = new CategoryRepository();
            cbCattegory.DisplayMemberPath = "Name";
            cbCattegory.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = categoryRepository.GetAllCategories() });

            //Загрузка данных в Grid в новом потоке
            Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        }

        private void btnExpense_Click(object sender, RoutedEventArgs e)
        {
            btnExpense.Background = Brushes.Red;
            btnExpense.BorderBrush = Brushes.Red;
            btnIncome.Background = Brushes.Gray;
            btnIncome.BorderBrush = Brushes.Gray;
            isExpense = true;
        }

        private void btnIncome_Click(object sender, RoutedEventArgs e)
        {
            //TODO:Ask Ihor how to do it wright, Discuss advisability of one interface for expense and income
            btnIncome.Background = Brushes.Green;
            btnIncome.BorderBrush = Brushes.Green;
            btnExpense.Background = Brushes.Gray;
            btnExpense.BorderBrush = Brushes.Gray;
            isExpense = false;
        }

        private void LoadData()
        {
            ExpensesRepository expensesRepository = new ExpensesRepository();
            dataGrid.ItemsSource = expensesRepository.GetAllExpenses();
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, true) && isAdd)
            {
                ExpensesRepository expensesRepository = new ExpensesRepository();
                var cashAccountSelectedItem = cbCashAccount.SelectedItem as CashAccount;
                //TODO: Fix DateTime output format
                expensesRepository.AddExpense(cashAccountSelectedItem.Id, Convert.ToDecimal(tbAmount.Text), cbCattegory.Text, DateTime.ParseExact(dpDate.Text, "d", null).Date, tbDescription.Text);
                Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
                cbCashAccount.Text = "";
                tbAmount.Text = "";
                tbDescription.Text = "";
                cbCattegory.Text = "";
                dpDate.Text = "";
            }
            //TODO:Ask Ihor how to do it wright
            else if (Equals(eventArgs.Parameter, true) && !isAdd)
            {

            }

        }

        private void btnUpdateTransaction_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            Expense selectedExpense = (Expense)dataGrid.SelectedItem;
            cbCashAccount.Text = selectedExpense.Cash;
            tbAmount.Text = selectedExpense.Amount.ToString();
            tbDescription.Text = selectedExpense.Description;
            cbCattegory.Text = selectedExpense.Category;
            dpDate.Text = selectedExpense.Date.Date.ToString();
        }

        private void btnDeleteTransaction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            isAdd = true;
        }
    }
}

