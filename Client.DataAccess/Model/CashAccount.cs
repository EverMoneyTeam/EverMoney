using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class CashAccount : BaseModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsJointCashAccount { get; set; }

        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public CashAccount()
        {
            Cashflows = new HashSet<Cashflow>();
        }
    }
}