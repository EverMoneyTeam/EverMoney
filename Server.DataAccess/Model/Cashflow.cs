using System;

namespace Server.DataAccess.Model
{
    public class Cashflow : BaseModel
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public Guid CashAccountId { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public Guid CashflowCategoryId { get; set; }

        public virtual CashflowCategory CashflowCategory { get; set; }

        public Guid? AccountId { get; set; }

        public Account Account { get; set; }
    }
}
