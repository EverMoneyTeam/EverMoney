using System;
using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class CashflowCategory : BaseModel
    {
        public string Name { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }
        
        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public Guid? ParentCashflowCategoryId { get; set; }

        public virtual CashflowCategory ParentCashflowCategory { get; set; }

        public virtual ICollection<CashflowCategory> ChildrenCashflowCategories { get; set; }

        public CashflowCategory()
        {
            Cashflows = new HashSet<Cashflow>();
            ChildrenCashflowCategories = new HashSet<CashflowCategory>();
        }
    }
}