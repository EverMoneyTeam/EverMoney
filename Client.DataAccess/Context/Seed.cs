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
        public const string DefaultAccountId = "00000000-0000-0000-0000-000000000000";
        public const string CurrencyId1 = "00000000-0000-0000-0000-000000000001";
        public const string CurrencyId2 = "00000000-0000-0000-0000-000000000002";
        public const string CurrencyId3 = "00000000-0000-0000-0000-000000000003";

        public static void SeedMethod()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                if (db.Accounts.Any() && db.Currencies.Any()) return;
                string cashAccountId1 = Guid.NewGuid().ToString();
                string catergoryId1 = Guid.NewGuid().ToString();
                string catergoryId2 = Guid.NewGuid().ToString();
                string catergoryId3 = Guid.NewGuid().ToString();
                string catergoryId4 = Guid.NewGuid().ToString();

                db.Accounts.Add(new Account {Id = DefaultAccountId, Login = "Guest", IsCurrent = true});

                //db.CashFlowCategories.Add(new CashFlowCategory { Id = MainCategoryId, Name = "All categories", AccountId = DefaultAccountId, DirtyFlag = true });

                //db.CashAccounts.Add(new CashAccount() { Id = cashAccountId1, Name = "PrivatBank", Amount = 10000, CurrencyId = currencyId1, AccountId = accountId, DirtyFlag = true });

                //db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -1000, Date = DateTime.Now, Description = "Скупился в магазине", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId2, AccountId  = accountId, DirtyFlag = true});
                //db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -100, Date = DateTime.Now.AddDays(-5), Description = "Обед", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId3, AccountId  = accountId, DirtyFlag = true });
                //db.CashFlows.Add(new CashFlow() { Id = Guid.NewGuid().ToString(), Amount = -6000, Date = DateTime.Now.AddDays(-2), Description = "Скупился одеждой", CashAccountId = cashAccountId1, CashFlowCategoryId = catergoryId4, AccountId = accountId, DirtyFlag = true });

                //db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId1, Name = "Еда", AccountId = accountId, DirtyFlag = true });
                //db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId2, Name = "Продукты питания", AccountId = accountId, ParentCashflowCategoryId = catergoryId1, DirtyFlag = true });
                //db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId3, Name = "Обеды, перекусы", AccountId = accountId, ParentCashflowCategoryId = catergoryId1, DirtyFlag = true });
                //db.CashFlowCategories.Add(new CashFlowCategory() { Id = catergoryId4, Name = "Одежда", AccountId = accountId, DirtyFlag = true });

                db.Currencies.Add(new Currency() { Id = CurrencyId1, Code = "1", Name = "UAH" });
                db.Currencies.Add(new Currency() { Id = CurrencyId2, Code = "2", Name = "USD" });
                db.Currencies.Add(new Currency() { Id = CurrencyId3, Code = "3", Name = "EUR" });

                db.SaveChanges();
            }
        }
    }
}
