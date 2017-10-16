using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class CashFlowCategory : BaseModel
    {
        public string Name { get; set; }

        public string AccountId { get; set; }

        public Account Account { get; set; }

        public virtual ICollection<CashFlow> CashFlows { get; set; }

        public CashFlowCategory()
        {
            CashFlows = new HashSet<CashFlow>();
        }
    }
}