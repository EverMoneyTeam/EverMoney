using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class CashflowCategory : BaseModel
    {
        public string Name { get; set; }

        public string AccountId { get; set; }

        public Account Account { get; set; }

        public virtual ICollection<Cashflow> Cashflows { get; set; }

        public CashflowCategory()
        {
            Cashflows = new HashSet<Cashflow>();
        }
    }
}