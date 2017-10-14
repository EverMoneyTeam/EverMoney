using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Model
{
    public class Account : BaseModel
    {

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<CashflowCategory> CashflowCategories { get; set; }

        public Account()
        {
            CashAccounts = new HashSet<CashAccount>();
            CashflowCategories = new HashSet<CashflowCategory>();
        }
    }
}
