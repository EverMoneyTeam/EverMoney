using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    //public class CashFlow
    //{
    //    public string Id { get; set; }
    //    public string CashFlowCategory { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Currency { get; set; }
    //    public DateTime Date { get; set; }
    //    public string CashAccount { get; set; }
    //    public string Description { get; set; }
    //}

    public class CashFlowsRepository
    {
        HistoryChangesRepository _changesRepository = new HistoryChangesRepository();

        public List<CashFlow> GetAllCashFlows()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlows.Where(c => c.Amount < 0).Include(c => c.CashAccount.Currency).Include(c => c.CashFlowCategory).ToList();
            }
        }

        public void AddCashFlow(string cashAccountId, decimal amount, string CashFlowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var CashFlow = new Model.CashFlow()
                {
                    Id = Guid.NewGuid().ToString(),
                    CashAccountId = cashAccountId,
                    Amount = -Math.Abs(amount),
                    CashFlowCategoryId = CashFlowCategoryId,
                    Date = date,
                    Description = description
                };

                db.CashFlows.Add(CashFlow);
                db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount -= amount;

                if (db.SaveChanges() > 0)
                {
                    _changesRepository.AddCashFlow(db, CashFlow);
                }
            }
        }

        public void UpdateCashFlow(string id, string cashAccountId, decimal amount, string CashFlowCategoryId, DateTime date, string description)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var result = db.CashFlows.SingleOrDefault(c => c.Id == id);

                decimal temp = result.Amount;

                result.CashAccountId = cashAccountId;
                result.Amount = amount;
                result.CashFlowCategoryId = CashFlowCategoryId;
                result.Date = date;
                result.Description = description;

                db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount = db.CashAccounts.FirstOrDefault(x => x.Id == cashAccountId).Amount + temp - amount;

                if (db.SaveChanges() > 0)
                {
                    // TODO History
                }
            }
        }

        public void DeleteCashFlow(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var result = db.CashFlows.SingleOrDefault(c => c.Id == id);

                db.CashAccounts.FirstOrDefault(x => x.Id == result.CashAccountId).Amount += result.Amount;

                db.CashFlows.Remove(result);
                db.SaveChanges();
            }
        }
    }
}
