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

        public static List<CashAccount> GetModifiedCashAccounts()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashAccounts.Where(c => c.DirtyFlag).ToList();
            }
        }

        public static bool AddCashAccount(string accountId, string currencyId, string name)
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

        public static bool AddSyncCashAccount(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                db.CashAccounts.Add(new CashAccount()
                {
                    Id = id,
                    USN = usn
                });

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateSyncCashAccount(string id, int usn, string accountId = null, string currencyId = null, string name = null, string amount = null)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);
                if (cashAccount == null) return false;

                if (accountId != null) cashAccount.AccountId = accountId;
                if (currencyId != null) cashAccount.CurrencyId = currencyId;
                if (name != null) cashAccount.Name = name;
                if (amount != null && decimal.TryParse(amount, out decimal a)) cashAccount.Amount = a;

                cashAccount.USN = usn;

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateCashAccount(string id, string currencyId, string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);
                if (cashAccount == null) return false;

                cashAccount.CurrencyId = currencyId;
                cashAccount.Name = name;
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

        public static bool DeleteSyncCashAccount(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);

                if (cashAccount == null) return false;

                db.CashAccounts.Remove(cashAccount);

                return db.SaveChanges() > 0;
            }
        }

        public static bool UnflagCashAccount(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccount = db.CashAccounts.FirstOrDefault(c => c.Id == id);

                if (cashAccount == null) return false;

                cashAccount.USN = usn;
                cashAccount.DirtyFlag = false;

                return db.SaveChanges() > 0;
            }
        }
    }
}
