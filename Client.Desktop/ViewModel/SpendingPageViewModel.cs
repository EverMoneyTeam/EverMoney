using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;
using Client.Desktop.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Desktop.ViewModel
{

    public class SpendingPageViewModel : BaseViewModel
    {
        private ObservableCollection<CashFlow> _cashFlows;

        private CashFlow _cashFlow;

        private ObservableCollection<CashFlowCategory> _cashFlowCategories;

        private object _selectedItem;

        public ICommand RunAddDialogCommand => new RelayCommand(ExecuteRunAddDialog);

        public ICommand RunAddCategoryDialogCommand => new RelayCommand(ExecuteRunAddCategoryDialog);

        public ICommand RunUpdateDialogCommand => new RelayCommand(ExecuteRunUpdateDialog);

        public ICommand RunDeleteCategoryDialogCommand => new RelayCommand(ExecuteRunDeleteCategoryDialog);

        public ICommand RunDeleteDialogCommand => new RelayCommand(ExecuteRunDeleteDialog);

        public AddCategoryDialogViewModel addCategoryDialogViewModel;

        public AddCashFlowDialogViewModel addCashFlowDialogViewModel;

        public UpdateCashFlowDialogViewModel updateCashFlowDialogViewModel;

        public DeleteCategoryDialogViewModel deleteCategoryDialogViewModel; 

        public DeleteCashFlowDialogViewModel deleteCashFlowDialogViewModel;

        public SpendingPageViewModel()
        {
            RefreshData();
            if (CashFlows.Any())
                SelectedCashFlow = CashFlows[0];
        }

        public ObservableCollection<CashFlow> CashFlows
        {
            get => _cashFlows;
            set => this.MutateVerbose(ref _cashFlows, value, RaisePropertyChanged());
        }

        public CashFlow SelectedCashFlow
        {
            get => _cashFlow;
            set => this.MutateVerbose(ref _cashFlow, value, RaisePropertyChanged());
        }

        public ObservableCollection<CashFlowCategory> CashFlowCategories
        {
            get => _cashFlowCategories;
            set => this.MutateVerbose(ref _cashFlowCategories, value, RaisePropertyChanged());
        }

        public object SelectedCategory
        {
            get => _selectedItem;
            set => this.MutateVerbose(ref _selectedItem, value, RaisePropertyChanged());
        }

        private async void ExecuteRunAddCategoryDialog(object o)
        {
            //Chack if there is selected category
            addCategoryDialogViewModel = new AddCategoryDialogViewModel();
            var view = new AddCategoryDialog
            {
                DataContext = addCategoryDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingAddCategoryDialogEventHandler);

            //check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private void ClosingAddCategoryDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                CashFlowCategoryRepository.AddCashFlowCategory(Properties.Login.Default.AccountId, addCategoryDialogViewModel.SelectedParentCashFlowCategory?.Id, addCategoryDialogViewModel.CashFlowCategory);
                RefreshData();
            }
        }

        private async void ExecuteRunAddDialog(object o)
        {
            addCashFlowDialogViewModel = new AddCashFlowDialogViewModel();
            var view = new AddCashFlowDialog
            {

                DataContext = addCashFlowDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingAddDialogEventHandler);

            //check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingAddDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                //TODO: add check
                var cashAccountSelectedItem = addCashFlowDialogViewModel.SelectedCashAccount as CashAccount;
                var categorySelectedItem = addCashFlowDialogViewModel.SelectedCashFlowCategory as CashFlowCategory;

                CashFlowRepository.AddCashFlow(cashAccountSelectedItem.Id, -Math.Abs(addCashFlowDialogViewModel.Amount), categorySelectedItem.Id, Convert.ToDateTime(addCashFlowDialogViewModel.Date), addCashFlowDialogViewModel.Description);
                //Renue content DataGrid
                RefreshData();
                //Set to zero fields
                addCashFlowDialogViewModel = new AddCashFlowDialogViewModel();
            }
        }

        private async void ExecuteRunUpdateDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            updateCashFlowDialogViewModel = new UpdateCashFlowDialogViewModel(SelectedCashFlow);
            var view = new UpdateCashFlowDialog
            {

                DataContext = updateCashFlowDialogViewModel
            };


            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingUpdateDialogEventHandler);

            //check the result...
            //Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingUpdateDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                var cashAccountSelectedItem = updateCashFlowDialogViewModel.SelectedCashAccount as CashAccount;
                var categorySelectedItem = updateCashFlowDialogViewModel.SelectedCashFlowCategory as CashFlowCategory;

                CashFlowRepository.UpdateCashFlow(SelectedCashFlow.Id, cashAccountSelectedItem.Id, -Math.Abs(updateCashFlowDialogViewModel.Amount), categorySelectedItem.Id, Convert.ToDateTime(updateCashFlowDialogViewModel.Date), updateCashFlowDialogViewModel.Description);
                //Renue content DataGrid
                RefreshData();
            }
        }

        private async void ExecuteRunDeleteCategoryDialog(object o)
        {
            var category = SelectedCategory as CashFlowCategory;
            deleteCategoryDialogViewModel = new DeleteCategoryDialogViewModel(category);
            var view = new DeleteCategoryDialog
            {
                DataContext = deleteCategoryDialogViewModel
            };
            
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteCategoryDialogEventHandler);
        }

        private void ClosingDeleteCategoryDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                var category = SelectedCategory as CashFlowCategory;
                CashFlowCategoryRepository.DeleteCashFlowCategory(category.Id);
                //Renue content Categories TreeView
                RefreshData();
            }
        }

        private void RefreshData()
        {
            CashFlowCategories = new ObservableCollection<CashFlowCategory>(CashFlowCategoryRepository.GetAllParentCashFlowCategories());
            CashFlows = new ObservableCollection<CashFlow>(CashFlowRepository.GetAllCashFlows(Properties.Login.Default.AccountId));
        }

        private async void ExecuteRunDeleteDialog(object o)
        {
            deleteCashFlowDialogViewModel = new DeleteCashFlowDialogViewModel(SelectedCashFlow);
            var view = new DeleteCashFlowDialog
            {
                DataContext = deleteCashFlowDialogViewModel
            };
            
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
        }

        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                CashFlowRepository.DeleteCashFlow(SelectedCashFlow.Id);
                //Renue content DataGrid
                RefreshData();
            }
        }

    }
}
