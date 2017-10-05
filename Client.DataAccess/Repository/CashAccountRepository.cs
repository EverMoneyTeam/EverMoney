using Client.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class CashAccount
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyId { get; set; }

        public string UserId { get; set; }
    }
    public class CashAccountRepository
    {
        public List<CashAccount> GetAllCashAccounts()
        {
            using(var db = new DatabaseContext())
            {
                var query = from cashAccount in db.CashAccounts
                            select new CashAccount
                            {
                                Id = cashAccount.Id,
                                Name = cashAccount.Name,
                                Amount = cashAccount.Amount,
                                CurrencyId = cashAccount.CurrencyId,
                                UserId = cashAccount.UserId
                            };

                return query.ToList();
            }
        }

    }
}
