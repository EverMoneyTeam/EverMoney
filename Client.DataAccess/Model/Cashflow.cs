    using System;

namespace Client.DataAccess.Model
{
    public class CashFlow : BaseModel
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CashAccountId { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public string CashFlowCategoryId { get; set; }

        public virtual CashFlowCategory CashFlowCategory { get; set; }
    }
}
