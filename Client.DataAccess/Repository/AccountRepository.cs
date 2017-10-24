using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.DataAccess.Context;
using Client.DataAccess.Model;

namespace Client.DataAccess.Repository
{
    public static class AccountRepository
    {
        public static Account GetAccount(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.Accounts.FirstOrDefault(a => a.Id == id);
            }
        }
        public static List<Account> GetAllAccounts()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.Accounts.ToList();
            }
        }

        public static void UpdateDefaultAccount(string accountId)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashAccounts = db.CashAccounts.Where(a => a.AccountId == Seed.DefaultAccountId).ToList();
                foreach (var cashAccount in cashAccounts)
                {
                    cashAccount.AccountId = accountId;
                }
                var cashFlowCategories = db.CashFlowCategories.Where(a => a.AccountId == Seed.DefaultAccountId).ToList();
                foreach (var cashFlowCategory in cashFlowCategories)
                {
                    cashFlowCategory.AccountId = accountId;
                }
                var cashFlows = db.CashFlows.Where(a => a.AccountId == Seed.DefaultAccountId).ToList();
                foreach (var cashFlow in cashFlows)
                {
                    cashFlow.AccountId = accountId;
                }
                db.Entry(db.Accounts.First(a => a.Id == Seed.DefaultAccountId)).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static void AddAccount(Account account)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }

        public static void SetLoginAccount(string accountId, string refreshToken)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var oldCurrentAccount = db.Accounts.FirstOrDefault(a => a.IsCurrent);
                if (oldCurrentAccount != null) oldCurrentAccount.IsCurrent = false;
                var newCurrentAccount = db.Accounts.First(a => a.Id == accountId);
                newCurrentAccount.IsCurrent = true;
                newCurrentAccount.RefreshToken = refreshToken;
                db.SaveChanges();
            }
        }
    }
}
