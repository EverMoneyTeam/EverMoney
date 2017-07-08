using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class CashAccount
    {
        public int CashAccountId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public int? AccountId { get; set; }

        public Account Account { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public bool IsJointCashAccount { get; set; }

        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public CashAccount()
        {
            Cashflows = new HashSet<Cashflow>();
        }
    }
}