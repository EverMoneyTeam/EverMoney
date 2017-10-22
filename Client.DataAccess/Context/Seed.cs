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
                string catergoryId3 = Guid.NewGuid().ToString();

                db.Accounts.Add(new Account {Id = accountId, Login = "Guest", IsCurrent = true});

                db.CashAccounts.Add(new CashAccount() { Id = cashAccountId1, Name = "PrivatBank", Amount = 10000, CurrencyId = currencyId1, AccountId = accountId, DirtyFlag = true });

                db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -1000, Date = DateTime.Now, Description = "Скупился в магазине", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId1, AccountId  = accountId, DirtyFlag = true});
                db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -100, Date = DateTime.Now.AddDays(-5), Description = "Обед", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId2, AccountId  = accountId, DirtyFlag = true });
                db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -6000, Date = DateTime.Now.AddDays(-2), Description = "Скупился одеждой", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId3, AccountId = accountId, DirtyFlag = true });

                db.CashFlowCategories.Add(new CashFlowCategory() { Id = Guid.NewGuid().ToString(), Name = "Еда", AccountId = accountId, DirtyFlag = true });
                db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId1, Name = "Продукты питания", AccountId = accountId, ParentCashflowCategoryId = catergoryId1, DirtyFlag = true });
                db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId2, Name = "Обеды, перекусы", AccountId = accountId, ParentCashflowCategoryId = catergoryId1, DirtyFlag = true });
                db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId3, Name = "Одежда", AccountId = accountId, DirtyFlag = true });

                db.Currencies.Add(new Currency() { Id = currencyId1, Code = "1", Name = "UAH" });
                db.Currencies.Add(new Currency() { Id = currencyId2, Code = "2", Name = "USD" });
                db.Currencies.Add(new Currency() { Id = currencyId3, Code = "3", Name = "EUR" });

                db.SaveChanges();
            }
        }
    }
}
