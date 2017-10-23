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
    public static class CashFlowCategoryRepository
    {
        public static List<CashFlowCategory> GetAllCashFlowCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.ToList();
            }
        }

        public static List<CashFlowCategory> GetAllParentCashFlowCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.Where(c => c.ParentCashflowCategoryId == null).Include(c => c.ChildrenCashflowCategories).ToList();
            }
        }

        public static CashFlowCategory GetCashFlowCategoryByName(string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.FirstOrDefault(x => x.Name == name);
            }
        }

        public static void AddCategory(string accountId, string id, string cashFlowCategory)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var Category = new CashFlowCategory()
                {
                    AccountId = accountId,
                    Name = cashFlowCategory,
                    ParentCashflowCategoryId = id
                };
                db.CashFlowCategories.Add(Category);
                db.SaveChanges();
            }
        }

        public static void DeleteCategory(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var result = db.CashFlowCategories.SingleOrDefault(c => c.Id == id);
                db.CashFlowCategories.Remove(result);
                db.SaveChanges();
            }
        }
    }
}
