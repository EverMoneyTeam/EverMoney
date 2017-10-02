using System.Collections.Generic;

namespace Client.DataAccess.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public User()
        {
            CashAccounts = new HashSet<CashAccount>();
        }
    }
}
