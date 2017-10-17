using System;
using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class CashFlowCategory : BaseModel
    {
        public string Name { get; set; }

        public string AccountId { get; set; }

        public Account Account { get; set; }

        public virtual ICollection<CashFlow> CashFlows { get; set; }

        public string ParentCashflowCategoryId { get; set; }

        public virtual CashFlowCategory ParentCashflowCategory { get; set; }

        public virtual ICollection<CashFlowCategory> ChildrenCashflowCategories { get; set; }

        public CashFlowCategory()
        {
            CashFlows = new HashSet<CashFlow>();
        }
    }
}