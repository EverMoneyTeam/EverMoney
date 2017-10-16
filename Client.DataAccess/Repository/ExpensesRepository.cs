﻿using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class Expense
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Cash { get; set; }
        public string Description { get; set; }
    }

    public class ExpensesRepository
    {
        HistoryChangesRepository _changesRepository = new HistoryChangesRepository();

        public List<Expense> GetAllExpenses()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var query = from cashFlows in db.Cashflows

                            join cashAccount in db.CashAccounts
                                on cashFlows.CashAccountId equals cashAccount.Id

                            join cfCategory in db.CashflowCategories
                                on cashFlows.CashflowCategoryId equals cfCategory.Id

                            join currency in db.Currencies
                                on cashAccount.CurrencyId equals currency.Id

                            where cashFlows.Amount < 0

                            select new Expense
                            {
                                Id = cashFlows.Id,
                                Category = cfCategory.Name,
                                Amount = Math.Abs(cashFlows.Amount),
                                Currency = currency.Name,
                                Date = cashFlows.Date,
                                Cash = cashAccount.Name,
                                Description = cashFlows.Description
                            };

                return query.ToList();
            }
        }

        public void AddExpense(string cashAccountId, decimal amount, string cashflowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashflow = new Cashflow()
                {
                    Id = Guid.NewGuid().ToString(),
                    CashAccountId = cashAccountId,
                    Amount = -amount,
                    CashflowCategoryId = cashflowCategoryId,
                    Date = date,
                    Description = description
                };

                db.Cashflows.Add(cashflow);

                db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount -= amount;

                if (db.SaveChanges() > 0)
                {
                    _changesRepository.AddCashflow(db, cashflow);
                }
            }
        }

        public void UpdateExpense(string id, string cashAccountId, decimal amount, string cashflowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var result = db.Cashflows.SingleOrDefault(c => c.Id == id);

                decimal temp = result.Amount;

                result.CashAccountId = cashAccountId;
                result.Amount = amount;
                result.CashflowCategoryId = cashflowCategoryId;
                result.Date = date;
                result.Description = description;

                db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount = db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount + temp - amount;

                if (db.SaveChanges() > 0)
                {
                    // TODO History
                }
            }
        }
    }
}
