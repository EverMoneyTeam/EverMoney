using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public static class CashAccountRepository
    {
        public static List<CashAccount> GetAllCashAccounts()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashAccounts.Where(c => c.USN > -1).ToList();
            }
        }

        public static bool AddCashAccount(string accountId, string currencyId = null, string name = null)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                db.CashAccounts.Add(new CashAccount()
                {
                    AccountId = accountId,
                    CurrencyId = currencyId,
                    Name = name,
                    DirtyFlag = true
                });

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateCashAccount(string id, string currencyId, string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);
                if (cashAccount == null) return false;

                if (currencyId != null) cashAccount.CurrencyId = currencyId;
                if (name != null) cashAccount.Name = name;
                cashAccount.DirtyFlag = true;

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashAccount(string id)
        {
            bool success;

            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);
                if (cashAccount == null) return false;

                cashAccount.USN = -1;
                cashAccount.DirtyFlag = true;

                success = db.SaveChanges() > 0;
            }

            return success && CashFlowRepository.DeleteCashAccount(id);
        }

        public static bool RemoveCashAccount(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);

                if (cashAccount == null) return false;

                db.CashAccounts.Remove(cashAccount);

                return db.SaveChanges() > 0;
            }
        }
    }
}
