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
    public class AddCategoryDialogViewModel : BaseViewModel
    {
        private CashFlowCategory _parentCashFlowCategory;
        private ObservableCollection<CashFlowCategory> _cashFlowCategories = new ObservableCollection<CashFlowCategory>();

        private string _cashFlowCategory;

        public AddCategoryDialogViewModel()
        {
            var allParentCashFlowCategories = CashFlowCategoryRepository.GetAllCashFlowCategories();
            ParentCashFlowCategories = new ObservableCollection<CashFlowCategory>(allParentCashFlowCategories);
        }
        public CashFlowCategory SelectedParentCashFlowCategory
        {
            get { return _parentCashFlowCategory; }
            set
            {
                this.MutateVerbose(ref _parentCashFlowCategory, value, RaisePropertyChanged());
            }
        }

        public ObservableCollection<CashFlowCategory> ParentCashFlowCategories
        {
            get { return _cashFlowCategories; }
            set
            {
                this.MutateVerbose(ref _cashFlowCategories, value, RaisePropertyChanged());
            }
        }

        public string CashFlowCategory
        {
            get { return _cashFlowCategory; }
            set
            {
                this.MutateVerbose(ref _cashFlowCategory, value, RaisePropertyChanged());
            }
        }
    }
}
