using System.Collections.ObjectModel;
using Client.DataAccess.Model;
using Client.Desktop.Helper;

namespace Client.Desktop.ViewModel
{
    public class UpdateCashAccountDialogViewModel : BaseViewModel
    {
        private Currency _cashFlowCategory;

        private ObservableCollection<Currency> _currencies;

        private decimal _amount;

        private string _name;
        private CashAccount selectedCashAccount;

        public UpdateCashAccountDialogViewModel(CashAccount selectedCashAccount)
        {
            this.selectedCashAccount = selectedCashAccount;
        }



        public Currency SelectedCurrency
        {
            get => _cashFlowCategory;
            set => this.MutateVerbose(ref _cashFlowCategory, value, RaisePropertyChanged());
        }

        public int SelectedCurrencyIndex { get; set; }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set => this.MutateVerbose(ref _currencies, value, RaisePropertyChanged());
        }

        public decimal Amount
        {
            get => _amount;
            set => this.MutateVerbose(ref _amount, value, RaisePropertyChanged());
        }

        public string Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }
    }
}