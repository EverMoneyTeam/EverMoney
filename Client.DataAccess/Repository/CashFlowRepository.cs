using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Client.DataAccess.Repository
{
    public static class CashFlowRepository
    {
        public static List<CashFlow> GetAllCashFlows(string accountId)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlows.Where(c => c.AccountId == accountId && c.Amount < 0 && c.USN > -1).Include(c => c.CashAccount.Currency).Include(c => c.CashFlowCategory).ToList();
            }
        }

        public static bool AddCashFlow(string cashAccountId, decimal amount, string cashFlowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = new CashFlow()
                {
                    Id = Guid.NewGuid().ToString(),
                    CashAccountId = cashAccountId,
                    Amount = amount,
                    CashFlowCategoryId = cashFlowCategoryId,
                    Date = date,
                    Description = description,
                    DirtyFlag = true
                };

                db.CashFlows.Add(cashFlow);

                var cashAccount = db.CashAccounts.FirstOrDefault(x => x.Id == cashFlow.CashAccountId);

                if (cashAccount == null) return false;

                cashAccount.Amount += cashFlow.Amount;

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateCashFlow(string id, string cashAccountId, decimal amount, string cashFlowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                var temp = cashFlow.AmountGrid;

                cashFlow.CashAccountId = cashAccountId;
                cashFlow.Amount = amount;
                cashFlow.CashFlowCategoryId = cashFlowCategoryId;
                cashFlow.Date = date;
                cashFlow.Description = description;
                cashFlow.DirtyFlag = true;

                var cashAccount = db.CashAccounts.FirstOrDefault(x => x.Id == cashFlow.CashAccountId);

                if (cashAccount == null) return false;

                cashAccount.Amount -= temp + amount;

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashFlow(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                var cashAccount = db.CashAccounts.FirstOrDefault(x => x.Id == cashFlow.CashAccountId);

                if (cashAccount == null) return false;

                cashAccount.Amount -= cashFlow.Amount;

                cashFlow.USN = -1;

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashAccount(string cashAccountId)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlows = db.CashFlows.Where(c => c.CashAccountId == cashAccountId).ToList();

                foreach (var cashFlow in cashFlows)
                {
                    cashFlow.USN = -1;
                }

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashFlowCategory(string cashFlowCategoryId)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlows = db.CashFlows.Where(c => c.CashFlowCategoryId == cashFlowCategoryId).ToList();

                foreach (var cashFlow in cashFlows)
                {
                    cashFlow.USN = -1;
                }

                return db.SaveChanges() > 0;
            }
        }

        public static bool RemoveCashFlow(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                db.CashFlows.Remove(cashFlow);

                return db.SaveChanges() > 0;
            }
        }
    }
}
