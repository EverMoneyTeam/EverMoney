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
                return db.CashFlowCategories.Where(c => c.USN > -1).ToList();
            }
        }

        public static List<CashFlowCategory> GetAllParentCashFlowCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.Where(c => c.ParentCashflowCategoryId == null && c.USN > -1).Include(c => c.ChildrenCashflowCategories).ToList();
            }
        }

        public static bool AddCashFlowCategory(string accountId, string parentId, string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var category = new CashFlowCategory()
                {
                    AccountId = accountId,
                    Name = name,
                    ParentCashflowCategoryId = parentId,
                    DirtyFlag = true
                };
                db.CashFlowCategories.Add(category);
                return db.SaveChanges() > 0;
            }
        }

        public static bool UpdateCashFlowCategory(string id, string parentId, string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var category = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (category == null) return false;

                category.Name = name;
                category.ParentCashflowCategoryId = parentId;
                category.DirtyFlag = true;

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashFlowCategory(string id)
        {
            bool success;

            using (var db = DbContextFactory.GetDbContext())
            {
                var category = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (category == null) return false;

                category.DirtyFlag = true;
                category.USN = -1;

                success = db.SaveChanges() > 0;
            }

            return success && CashFlowRepository.DeleteCashFlowCategory(id);
        }

        public static bool RemoveCashFlowCategory(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlowCategory = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (cashFlowCategory == null) return false;

                db.CashFlowCategories.Remove(cashFlowCategory);

                return db.SaveChanges() > 0;
            }
        }
    }
}
