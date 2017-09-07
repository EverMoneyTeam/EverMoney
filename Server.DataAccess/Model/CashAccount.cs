using System;
using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class CashAccount : BaseModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public Guid CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public Guid? AccountId { get; set; }

        public Account Account { get; set; }

        public Guid? UserId { get; set; }

        public User User { get; set; }

        public bool IsJointCashAccount { get; set; }

        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public CashAccount()
        {
            Cashflows = new HashSet<Cashflow>();
        }
    }
}