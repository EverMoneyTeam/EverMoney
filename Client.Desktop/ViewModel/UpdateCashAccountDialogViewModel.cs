using System.Collections.ObjectModel;
using System.Linq;
using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;

namespace Client.Desktop.ViewModel
{
    public class UpdateCashAccountDialogViewModel : BaseViewModel
    {
        private Currency _cashFlowCategory;

        private ObservableCollection<Currency> _currencies;

        private decimal _amount;

        private string _name;

        public UpdateCashAccountDialogViewModel(CashAccount selectedCashAccount)
        {
            Amount = selectedCashAccount.Amount;
            Name = selectedCashAccount.Name;
            Currencies = new ObservableCollection<Currency>(CurrencyRepository.GetAllCurrencies());
            SelectedCurrency = Currencies.First(c => c.Id == selectedCashAccount.CurrencyId);
            SelectedCurrencyIndex = Currencies.IndexOf(SelectedCurrency);
        }

        public int SelectedCurrencyIndex { get; set; }
        
        public Currency SelectedCurrency
        {
            get => _cashFlowCategory;
            set => this.MutateVerbose(ref _cashFlowCategory, value, RaisePropertyChanged());
        }

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