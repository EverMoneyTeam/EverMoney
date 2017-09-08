using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class Account : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public virtual ICollection<CashflowCategory> CashflowCategories { get; set; }

        public Account()
        {
            Users = new HashSet<User>();
            Tokens = new HashSet<Token>();
            CashAccounts = new HashSet<CashAccount>();
            CashflowCategories = new HashSet<CashflowCategory>();
        }
    }
}
