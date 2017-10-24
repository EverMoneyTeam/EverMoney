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
    public class Category : CashFlowCategory, INotifyPropertyChanged
    {
        private bool _isSelected;
        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                    if (IsSelected)
                    SelectedItem = this;
                }
            }
        }
                public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CashFlowsPageViewModel : BaseViewModel
    {
        private ObservableCollection<CashFlow> _cashFlows;

        private CashFlow _cashFlow;

        private Category _cashFlowCategory;

        private ObservableCollection<Category> _cashFlowCategories;

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

        public CashFlowsPageViewModel()
        {
            var allCashFlows = CashFlowRepository.GetAllCashFlows(Properties.Login.Default.AccountId);
            CashFlows = new ObservableCollection<CashFlow>(allCashFlows);
            if (CashFlows.Any())
                SelectedCashFlow = CashFlows[0];

            CashFlowCategories = ConvertToCategory(CashFlowCategoryRepository.GetAllParentCashFlowCategories());
        }

        public ObservableCollection<Category> ConvertToCategory(List<CashFlowCategory> list)
        {
            List<Category> result = new List<Category>();
            foreach (var item in list)
            {
                result.Add(new Category() { Id = item.Id, Name = item.Name, AccountId = item.AccountId, ParentCashflowCategoryId = item.ParentCashflowCategoryId, ChildrenCashflowCategories = item.ChildrenCashflowCategories });
            }

            return new ObservableCollection<Category>(result);
        }
        public ObservableCollection<CashFlow> CashFlows
        {
            get
            {
                return _cashFlows;
            }
            set
            {
                this.MutateVerbose(ref _cashFlows, value, RaisePropertyChanged());
            }
        }

        public CashFlow SelectedCashFlow
        {
            get
            {
                return _cashFlow;
            }
            set
            {
                this.MutateVerbose(ref _cashFlow, value, RaisePropertyChanged());
            }
        }

        public ObservableCollection<Category> CashFlowCategories
        {
            get
            {
                return _cashFlowCategories;
            }
            set
            {
                this.MutateVerbose(ref _cashFlowCategories, value, RaisePropertyChanged());
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return CashFlowCategories.FirstOrDefault(c => c.IsSelected);
            }
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
                CashFlowCategories = ConvertToCategory(CashFlowCategoryRepository.GetAllParentCashFlowCategories());
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
                CashFlows = new ObservableCollection<CashFlow>(CashFlowRepository.GetAllCashFlows(Properties.Login.Default.AccountId));
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
                CashFlows = new ObservableCollection<CashFlow>(CashFlowRepository.GetAllCashFlows(Properties.Login.Default.AccountId));
            }
        }

        private async void ExecuteRunDeleteCategoryDialog(object o)
        {
            deleteCategoryDialogViewModel = new DeleteCategoryDialogViewModel(SelectedCategory);
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
                CashFlowCategoryRepository.DeleteCashFlowCategory(SelectedCategory.Id);
                //Renue content Categories TreeView
                CashFlowCategories = ConvertToCategory(CashFlowCategoryRepository.GetAllParentCashFlowCategories());
            }
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
                CashFlows = new ObservableCollection<CashFlow>(CashFlowRepository.GetAllCashFlows(Properties.Login.Default.AccountId));
            }
        }

    }
}
