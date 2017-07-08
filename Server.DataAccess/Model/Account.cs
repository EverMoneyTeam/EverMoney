using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class Account
    {
        public int AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<CashflowCategory> CashflowCategories { get; set; }

        public Account()
        {
            Users = new HashSet<User>();
            CashAccounts = new HashSet<CashAccount>();
            CashflowCategories = new HashSet<CashflowCategory>();
        }
    }
}
