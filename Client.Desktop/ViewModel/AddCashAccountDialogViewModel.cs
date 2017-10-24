using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;

namespace Client.Desktop.ViewModel
{
    public class AddCashAccountDialogViewModel : BaseViewModel
    {
        private Currency _selectedCurrency;

        private ObservableCollection<Currency> _currencies;

        private decimal _amount;

        private string _name;

        public AddCashAccountDialogViewModel()
        {
            var currencies = CurrencyRepository.GetAllCurrencies();
            Currencies = new ObservableCollection<Currency>(currencies);
        }

        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set => this.MutateVerbose(ref _selectedCurrency, value, RaisePropertyChanged());
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
