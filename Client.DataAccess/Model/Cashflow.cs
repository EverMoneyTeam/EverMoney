    using System;
    using System.ComponentModel.DataAnnotations.Schema;

namespace Client.DataAccess.Model
{
    public class CashFlow : SyncModel
    {
        public decimal Amount { get; set; }

        [NotMapped]
        public decimal AmountGrid => Math.Abs(Amount);

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string AccountId { get; set; }

        public virtual Account Account { get; set; }

        public string CashAccountId { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public string CashFlowCategoryId { get; set; }

        public virtual CashFlowCategory CashFlowCategory { get; set; }
    }
}
