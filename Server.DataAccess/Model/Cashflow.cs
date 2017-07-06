using System;

namespace Server.DataAccess.Model
{
    public class Cashflow
    {
        public int CashflowId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int CashAccountId { get; set; }

        public virtual CashAccount CashAccount { get; set; }

        public int CashflowCategoryId { get; set; }

        public virtual CashflowCategory CashflowCategory { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
