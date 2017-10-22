using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class CashAccount : SyncModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public string AccountId { get; set; }

        public Account Account { get; set; }

        public virtual ICollection<CashFlow> CashFlows { get; set; }

        public CashAccount()
        {
            CashFlows = new HashSet<CashFlow>();
        }
    }
}