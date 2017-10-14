using Client.DataAccess.Repository;
using Client.Desktop.Helper;
using Client.Desktop.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Desktop.ViewModel
{
    public class ExpensesPageViewModel
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public ICommand RunDialogCommand => new RelayCommand(ExecuteRunDialog);

        private async void ExecuteRunDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new AddExpenseDialog
            {
                DataContext = new AddExpenseDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

        public ExpensesPageViewModel()
        {
            ExpensesRepository expensesRepository = new ExpensesRepository();
            Expenses = new ObservableCollection<Expense>(expensesRepository.GetAllExpenses());
        }
    }
}
