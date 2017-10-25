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

        public static List<CashFlow> GetCashFlowsByCategory(string accountId, string categoryId)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var list = db.CashFlows.Where(c => c.AccountId == accountId && c.Amount < 0 && c.USN > -1 && c.CashFlowCategoryId == categoryId).Include(c => c.CashAccount.Currency).Include(c => c.CashFlowCategory).ToList();
                var childrenCashFlowCategories = CashFlowCategoryRepository.GetAllChildrenCashFlowCategoriesRecursively(categoryId);
                foreach (var item in childrenCashFlowCategories)
                {
                    var cashFlows = db.CashFlows.Where(c => c.AccountId == accountId && c.Amount < 0 && c.USN > -1 && c.CashFlowCategoryId == item.Id).Include(c => c.CashAccount.Currency).Include(c => c.CashFlowCategory).ToList();
                    list.AddRange(cashFlows);
                }
                return list;
            }
        }

        public static List<CashFlow> GetModifiedCashFlows()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlows.Where(c => c.DirtyFlag).ToList();
            }
        }

        public static bool AddCashFlow(string accountId, string cashAccountId, decimal amount, string cashFlowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = new CashFlow()
                {
                    AccountId = accountId,
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
                cashAccount.DirtyFlag = true;

                return db.SaveChanges() > 0;
            }
        }

        public static bool AddSyncCashFlow(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null)
                {
                    db.CashFlows.Add(new CashFlow()
                    {
                        Id = id,
                        USN = usn
                    });
                }
                else
                {
                    if (cashFlow.DirtyFlag) return false;
                    cashFlow.USN = usn;
                }

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
                cashAccount.DirtyFlag = true;

                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateSyncCashFlow(string id, int usn, string accountId = null, string cashAccountId = null, string amount = null, string cashFlowCategoryId = null, string date = null, string description = null)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                if (accountId != null) cashFlow.AccountId = accountId;
                if (cashAccountId != null) cashFlow.CashAccountId = cashAccountId;
                if (amount != null && decimal.TryParse(amount, out decimal a)) cashFlow.Amount = a;
                if (cashFlowCategoryId != null) cashFlow.CashFlowCategoryId = cashFlowCategoryId;
                if (date != null && DateTime.TryParse(date, out DateTime d)) cashFlow.Date = d;
                if (description != null) cashFlow.Description = description;
                cashFlow.USN = usn;

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
                cashAccount.DirtyFlag = true;

                if (cashFlow.USN == 0)
                {
                    return DeleteSyncCashFlow(cashFlow.Id);
                }

                cashFlow.USN = -1;
                cashFlow.DirtyFlag = true;

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
                    if (cashFlow.USN == 0)
                    {
                        if (!DeleteSyncCashFlow(cashFlow.Id)) return false;
                    }
                    else
                    {
                        cashFlow.USN = -1;
                        cashFlow.DirtyFlag = true;
                    }
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
                    if (cashFlow.USN == 0)
                    {
                        if (!DeleteSyncCashFlow(cashFlow.Id)) return false;
                    }
                    else
                    {
                        cashFlow.USN = -1;
                        cashFlow.DirtyFlag = true;
                    }
                }

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteSyncCashFlow(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                db.CashFlows.Remove(cashFlow);

                return db.SaveChanges() > 0;
            }
        }

        public static bool UnflagCashFlow(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlow = db.CashFlows.FirstOrDefault(c => c.Id == id);

                if (cashFlow == null) return false;

                cashFlow.USN = usn;
                cashFlow.DirtyFlag = false;

                return db.SaveChanges() > 0;
            }
        }
    }
}
