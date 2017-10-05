using Client.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class Expense
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Cash { get; set; }
        public string Description { get; set; }
    }

    public class ExpensesRepository
    {
        public List<Expense> GetAllExpenses()
        {
            using (var db = new DatabaseContext())
            {
                var query = from cashFlows in db.Cashflows

                            join cashAccount in db.CashAccounts
                            on cashFlows.CashAccountId equals cashAccount.Id

                            join cfCategory in db.CashflowCategories
                            on cashFlows.CashflowCategoryId equals cfCategory.Id

                            join currency in db.Currencies
                            on cashAccount.CurrencyId equals currency.Id
                            select new Expense
                            {
                                Category = cfCategory.Name,
                                Amount = cashFlows.Amount,
                                Currency = currency.Name,
                                Date = cashFlows.Date,
                                Cash = cashAccount.Name,
                                Description = cashFlows.Description
                            };

                return query.ToList();
            }
        }

        public void AddExpense(string cashAccountId, decimal amount, string category, DateTime date, string description)
        {
            using (var db = new DatabaseContext())
            {
                var cashflowCategoryId = db.CashflowCategories.FirstOrDefault(x => x.Name == category).Id;

                db.Cashflows.Add(new Model.Cashflow() { Id = Guid.NewGuid().ToString(), CashAccountId = cashAccountId, Amount = amount, CashflowCategoryId = cashflowCategoryId, Date = date, Description = description });
                db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount -= amount;
                db.SaveChanges();
            }
        }

        //TODO: Update Method, Think about change tracker
        //public void UpdateExpense(string cashAccountId, decimal amount, string category, DateTime date, string description)
        //{
        //    using (var db = new DatabaseContext())
        //    {
        //        var cashflowCategoryId = db.CashflowCategories.FirstOrDefault(x => x.Name == category).Id;

        //        db.Cashflows.Add(new Model.Cashflow() { Id = Guid.NewGuid().ToString(), CashAccountId = cashAccountId, Amount = amount, CashflowCategoryId = cashflowCategoryId, Date = date, Description = description });
        //        db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount -= amount;
        //        db.SaveChanges();
        //    }
        //}


    }
}
