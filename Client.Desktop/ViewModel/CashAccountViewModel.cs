using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;
using Client.Desktop.View;
using MaterialDesignThemes.Wpf;

namespace Client.Desktop.ViewModel
{
    public class CashAccountViewModel : BaseViewModel
    {
        private ObservableCollection<CashAccount> _cashAccounts;

        private CashAccount _cashAccount;

        public ICommand RunAddDialogCommand => new RelayCommand(ExecuteRunAddDialog);

        public ICommand RunUpdateDialogCommand => new RelayCommand(ExecuteRunUpdateDialog);

        public ICommand RunDeleteDialogCommand => new RelayCommand(ExecuteRunDeleteDialog);

        public AddCashAccountDialogViewModel addCashAccountDialogViewModel;

        //public UpdateCashAccountDialogViewModel updateCashAccountDialogViewModel;

        //public DeleteCashAccountDialogViewModel deleteCashAccountDialogViewModel;


        public CashAccount SelectedCashAccount
        {
            get => _cashAccount;
            set => this.MutateVerbose(ref _cashAccount, value, RaisePropertyChanged());
        }

        public ObservableCollection<CashAccount> CashAccounts
        {
            get => _cashAccounts;
            set => this.MutateVerbose(ref _cashAccounts, value, RaisePropertyChanged());
        }

        private async void ExecuteRunAddDialog(object o)
        {
            addCashAccountDialogViewModel = new AddCashAccountDialogViewModel();
            var view = new AddCashFlowDialog
            {

                DataContext = addCashAccountDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingAddDialogEventHandler);

            //check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void ExecuteRunUpdateDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            //updateCashAccountDialogViewModel = new UpdateCashAccountDialogViewModel(SelectedCashAccount);
            //var view = new UpdateCashFlowDialog
            //{

            //    DataContext = updateCashAccountDialogViewModel
            //};


            ////show the dialog
            //var result = await DialogHost.Show(view, "RootDialog", ClosingUpdateDialogEventHandler);

            //check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void ExecuteRunDeleteDialog(object o)
        {
            //deleteCashAccountDialogViewModel = new DeleteCashAccountDialogViewModel(SelectedCashAccount);
            //var view = new DeleteCashFlowDialog
            //{
            //    DataContext = deleteCashAccountDialogViewModel
            //};

            ////show the dialog
            //var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
        }

        private void ClosingAddDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                //CashFlowRepository.AddCashFlow(cashAccountSelectedItem.Id, -Math.Abs(addCashAccountDialogViewModel.Amount), categorySelectedItem.Id, Convert.ToDateTime(addCashAccountDialogViewModel.Date), addCashAccountDialogViewModel.Description);
                //Renue content DataGrid
                RefreshData();
                //Set to zero fields
                addCashAccountDialogViewModel = new AddCashAccountDialogViewModel();
            }
        }

        private void ClosingUpdateDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                //var cashAccountSelectedItem = updateCashAccountDialogViewModel.SelectedCashAccount as CashAccount;
                //var categorySelectedItem = updateCashAccountDialogViewModel.SelectedCashFlowCategory as CashFlowCategory;

               // CashFlowRepository.UpdateCashFlow(SelectedCashAccount.Id, cashAccountSelectedItem.Id, -Math.Abs(updateCashAccountDialogViewModel.Amount), categorySelectedItem.Id, Convert.ToDateTime(updateCashAccountDialogViewModel.Date), updateCashAccountDialogViewModel.Description);
                //Renue content DataGrid
                RefreshData();
            }
        }

        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                CashFlowRepository.DeleteCashFlow(SelectedCashAccount.Id);
                //Renue content DataGrid
                RefreshData();
            }
        }

        private void RefreshData()
        {
            CashAccounts = new ObservableCollection<CashAccount>(CashAccountRepository.GetAllCashAccounts());
        }
    }
}
