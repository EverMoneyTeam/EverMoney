using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.DataAccess.Model;

namespace Client.DataAccess.Context
{
    public static  class Seed
    {

        public static void SeedMethod()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                if (db.Accounts.Any()) return;
                string accountId = "00000000-0000-0000-0000-000000000000";
                string currencyId1 = "00000000-0000-0000-0000-000000000001";
                string currencyId2 = "00000000-0000-0000-0000-000000000002";
                string currencyId3 = "00000000-0000-0000-0000-000000000003";
                string cashAccountId1 = Guid.NewGuid().ToString();
                string catergoryId1 = Guid.NewGuid().ToString();
                string catergoryId2 = Guid.NewGuid().ToString();

                db.Accounts.Add(new Account {Id = accountId, Login = "Guest", IsCurrent = true});

                db.CashAccounts.Add(new CashAccount() { Id = cashAccountId1, Name = "PrivatBank", Amount = 10000, CurrencyId = currencyId1, AccountId = accountId});

                db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -3000, Date = DateTime.Now, Description = "Скупился на месяц", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId1  });
                db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -6000, Date = DateTime.Now.AddDays(-2), Description = "Скупился одеждой", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId2 });

                db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId1, Name = "Продукты питания", AccountId = accountId});
                db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId2, Name = "Одежда", AccountId = accountId });

                db.Currencies.Add(new Currency() { Id = currencyId1, Code = "1", Name = "UAH" });
                db.Currencies.Add(new Currency() { Id = currencyId2, Code = "2", Name = "USD" });
                db.Currencies.Add(new Currency() { Id = currencyId3, Code = "3", Name = "EUR" });

                db.SaveChanges();
            }
        }
    }
}
