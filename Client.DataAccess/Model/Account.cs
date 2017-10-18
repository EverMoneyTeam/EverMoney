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

        public string RefreshToken { get; set; }

        public bool IsCurrent { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<CashFlowCategory> CashFlowCategories { get; set; }

        public virtual ICollection<CashFlow> CashFlow { get; set; }

        public virtual ICollection<HistoryChange> HistoryChanges { get; set; }

        public Account()
        {
            CashAccounts = new HashSet<CashAccount>();
            HistoryChanges = new HashSet<HistoryChange>();
            CashFlowCategories = new HashSet<CashFlowCategory>();
            CashFlow = new HashSet<CashFlow>();
        }
    }
}
