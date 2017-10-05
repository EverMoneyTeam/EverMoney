using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Context
{
    public static  class Seed
    {

        public static void SeedMethod()
        {
            using (var db = new DatabaseContext())
            {
                string currencyId_1 = Guid.NewGuid().ToString();
                string cashAccountId_1 = Guid.NewGuid().ToString();
                string catergoryId_1 = Guid.NewGuid().ToString();
                string catergoryId_2 = Guid.NewGuid().ToString();

                string userId_1 = Guid.NewGuid().ToString();

                db.Users.Add(new Model.User() { Id = userId_1, Name = "Я" });

                db.CashAccounts.Add(new Model.CashAccount() { Id = cashAccountId_1, Name = "PrivatBank", Amount = 10000, CurrencyId = currencyId_1, UserId = userId_1 });

                db.Cashflows.Add(new Model.Cashflow() { Id = Guid.NewGuid().ToString(), Amount = 3000, Date = DateTime.Now, Description = "Скупился на месяц", CashAccountId = cashAccountId_1, CashflowCategoryId = catergoryId_1  });
                db.Cashflows.Add(new Model.Cashflow() { Id = Guid.NewGuid().ToString(), Amount = 6000, Date = DateTime.Now.AddDays(-2), Description = "Скупился одеждой", CashAccountId = cashAccountId_1, CashflowCategoryId = catergoryId_2 });

                db.CashflowCategories.Add(new Model.CashflowCategory() { Id = catergoryId_1, Name = "Продукты питания" });
                db.CashflowCategories.Add(new Model.CashflowCategory() { Id = catergoryId_2, Name = "Одежда" });

                db.Currencies.Add(new Model.Currency() { Id = currencyId_1, Code = "1", Name = "UAH" });
                db.Currencies.Add(new Model.Currency() { Id = Guid.NewGuid().ToString(), Code = "2", Name = "USD" });
                db.Currencies.Add(new Model.Currency() { Id = Guid.NewGuid().ToString(), Code = "3", Name = "EUR" });

                db.SaveChanges();
            }
        }
    }
}
