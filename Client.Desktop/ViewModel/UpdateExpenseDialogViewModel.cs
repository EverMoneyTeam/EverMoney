using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.ViewModel
{
    public class UpdateCashFlowDialogViewModel : BaseViewModel
    {
        private CashAccount _cashAccount;
        private ObservableCollection<CashAccount> _cashAccounts = new ObservableCollection<CashAccount>();

        private CashFlowCategory _cashFlowCategory;
        private ObservableCollection<CashFlowCategory> _cashFlowCategories = new ObservableCollection<CashFlowCategory>();

        private decimal _amount;

        private string _date = DateTime.Now.ToString();

        private string _description;

        public CashAccount SelectedCashAccount
        {
            get => _cashAccount;
            set => this.MutateVerbose(ref _cashAccount, value, RaisePropertyChanged());
        }

        public int SelectedCashAccountIndex { get; set; }

        public ObservableCollection<CashAccount> CashAccounts
        {
            get => _cashAccounts;
            set => this.MutateVerbose(ref _cashAccounts, value, RaisePropertyChanged());
        }

        public CashFlowCategory SelectedCashFlowCategory
        {
            get => _cashFlowCategory;
            set => this.MutateVerbose(ref _cashFlowCategory, value, RaisePropertyChanged());
        }

        public int SelectedCashFlowCategoryIndex { get; set; }

        public ObservableCollection<CashFlowCategory> CashFlowCategories
        {
            get => _cashFlowCategories;
            set => this.MutateVerbose(ref _cashFlowCategories, value, RaisePropertyChanged());
        }


        public decimal Amount
        {
            get => _amount;
            set => this.MutateVerbose(ref _amount, value, RaisePropertyChanged());
        }

        public string Date
        {
            get => _date;
            set => this.MutateVerbose(ref _date, value, RaisePropertyChanged());
        }

        public string Description
        {
            get => _description;
            set => this.MutateVerbose(ref _description, value, RaisePropertyChanged());
        }

        public UpdateCashFlowDialogViewModel(CashFlow selectedCashFlow)
        {
            //Initialise all fields in dialog, depending on selected CashFlow from DataGrid
            var allCashAccounts = CashAccountRepository.GetAllCashAccounts();
            var allCashFlowCategories = CashFlowCategoryRepository.GetAllCashFlowCategories();

            //TODO: is it right to look for cashAccount by Name
            SelectedCashAccount = selectedCashFlow.CashAccount;

            SelectedCashAccountIndex = allCashAccounts.FindIndex(x => x.Name == selectedCashFlow.CashAccount.Name);

            CashAccounts = new ObservableCollection<CashAccount>(allCashAccounts);

            Amount = selectedCashFlow.Amount;
            
            SelectedCashFlowCategory = selectedCashFlow.CashFlowCategory;

            SelectedCashFlowCategoryIndex = allCashFlowCategories.FindIndex(x => x.Name == selectedCashFlow.CashFlowCategory.Name);

            CashFlowCategories = new ObservableCollection<CashFlowCategory>(CashFlowCategoryRepository.GetAllCashFlowCategories());

            Date = selectedCashFlow.Date.ToString();

            Description = selectedCashFlow.Description;
        }
    }
}
