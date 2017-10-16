﻿using Client.DataAccess.Model;
using Client.DataAccess.Repository;
using Client.Desktop.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Desktop.ViewModel
{
    public class AddCashFlowDialogViewModel : BaseViewModel
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
            get { return _cashAccount; }
            set
            {
                this.MutateVerbose(ref _cashAccount, value, RaisePropertyChanged());
            }
        }

        public int SelectedCashAccountIndex { get; set; }

        public ObservableCollection<CashAccount> CashAccounts
        {
            get { return _cashAccounts; }
            set
            {
                this.MutateVerbose(ref _cashAccounts, value, RaisePropertyChanged());
            }
        }

        public CashFlowCategory SelectedCashFlowCategory
        {
            get { return _cashFlowCategory; }
            set
            {
                this.MutateVerbose(ref _cashFlowCategory, value, RaisePropertyChanged());
            }
        }

        public int SelectedCashFlowCategoryIndex { get; set; }

        public ObservableCollection<CashFlowCategory> CashFlowCategories
        {
            get { return _cashFlowCategories; }
            set
            {
                this.MutateVerbose(ref _cashFlowCategories, value, RaisePropertyChanged());
            }
        }


        public decimal Amount
        {
            get { return _amount; }
            set
            {
                this.MutateVerbose(ref _amount, value, RaisePropertyChanged());
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                this.MutateVerbose(ref _date, value, RaisePropertyChanged());
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                this.MutateVerbose(ref _description, value, RaisePropertyChanged());
            }
        }

        public AddCashFlowDialogViewModel()
        {
            CashAccountRepository cashAccountRepository = new CashAccountRepository();
            var allCashAccounts = cashAccountRepository.GetAllCashAccounts();
            CashAccounts = new ObservableCollection<CashAccount>(allCashAccounts);
            SelectedCashAccountIndex = 0;

            CashFlowCategoryRepository cashFlowCategoryRepository = new CashFlowCategoryRepository();
            var allCashFlowCategories = cashFlowCategoryRepository.GetAllCashFlowCategories();
            CashFlowCategories = new ObservableCollection<CashFlowCategory>(allCashFlowCategories);
            SelectedCashFlowCategoryIndex = 0;
        }
    }
}
