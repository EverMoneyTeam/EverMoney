using System.Collections.Generic;

namespace Server.DataAccess.Model
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<CashAccount> CashAccounts { get; set; }

        public Currency()
        {
            CashAccounts = new HashSet<CashAccount>();
        }
    }
}