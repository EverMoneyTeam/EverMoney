using System;

namespace Client.DataAccess.Model
{
    public class Cashflow : BaseModel
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CashAccountId { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public string CashflowCategoryId { get; set; }

        public virtual CashflowCategory CashflowCategory { get; set; }
    }
}
