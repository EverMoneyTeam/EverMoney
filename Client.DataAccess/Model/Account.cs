using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Model
{
    public class Account : BaseModel
    {
        public string Login { get; set; }

        public bool IsCurrent { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<CashflowCategory> CashflowCategories { get; set; }

        public virtual ICollection<HistoryChange> HistoryChanges { get; set; }

        public Account()
        {
            CashAccounts = new HashSet<CashAccount>();
            CashflowCategories = new HashSet<CashflowCategory>();
            HistoryChanges = new HashSet<HistoryChange>();
        }
    }
}
