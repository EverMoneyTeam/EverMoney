using Client.DataAccess.Model;

namespace Client.Desktop.ViewModel
{
    public class DeleteCashAccountDialogViewModel : BaseViewModel
    {
        private CashAccount selectedCashAccount;

        public DeleteCashAccountDialogViewModel(CashAccount selectedCashAccount)
        {
            this.selectedCashAccount = selectedCashAccount;
        }
    }
}