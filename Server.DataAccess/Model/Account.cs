using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class Account : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public virtual ICollection<CashflowCategory> CashflowCategories { get; set; }

        public virtual ICollection<HistoryChange> HistoryChanges { get; set; }

        public Account()
        {
            Tokens = new HashSet<Token>();
            CashAccounts = new HashSet<CashAccount>();
            Cashflows = new HashSet<Cashflow>();
            CashflowCategories = new HashSet<CashflowCategory>();
            HistoryChanges = new HashSet<HistoryChange>();
        }
    }
}
