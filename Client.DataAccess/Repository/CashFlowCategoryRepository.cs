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

        public static List<CashFlowCategory> GetModifiedCashFlowCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.Where(c => c.DirtyFlag).ToList();
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

        public static bool AddSyncCashFlowCategory(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var category = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    db.CashFlowCategories.Add(new CashFlowCategory()
                    {
                        Id = id,
                        USN = usn
                    });
                }
                else
                {
                    if (category.DirtyFlag) return false;
                    category.USN = usn;
                }

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

        public static bool UpdateSyncCashFlowCategory(string id, int usn, string accountId = null, string parentId = null, string name = null)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var category = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (category == null) return false;

                if (category.DirtyFlag) return false;

                if (name != null) category.Name = name;
                if (parentId != null) category.ParentCashflowCategoryId = parentId;
                if (accountId != null) category.AccountId = accountId;
                category.USN = usn;

                return db.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashFlowCategory(string id)
        {
            bool success;
            CashFlowCategory category;

            using (var db = DbContextFactory.GetDbContext())
            {
                category = db.CashFlowCategories.Include(c => c.ChildrenCashflowCategories).FirstOrDefault(c => c.Id == id);

                if (category == null) return false;

                if (category.USN == 0)
                {
                    success = DeleteSyncCashFlowCategory(category.Id);
                }
                else
                {
                    category.DirtyFlag = true;
                    category.USN = -1;

                    success = db.SaveChanges() > 0;
                }
            }

            if (success)
            {
                // Remove all child categories
                foreach (var a in category.ChildrenCashflowCategories)
                {
                    success = DeleteCashFlowCategory(a.Id);
                }
            }

            return success && CashFlowRepository.DeleteCashFlowCategory(id);
        }

        public static bool DeleteSyncCashFlowCategory(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlowCategory = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (cashFlowCategory == null) return false;

                db.CashFlowCategories.Remove(cashFlowCategory);

                return db.SaveChanges() > 0;
            }
        }

        public static bool UnflagCashFlowCategory(string id, int usn)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var cashFlowCategory = db.CashFlowCategories.FirstOrDefault(c => c.Id == id);

                if (cashFlowCategory == null) return false;

                cashFlowCategory.USN = usn;
                cashFlowCategory.DirtyFlag = false;

                return db.SaveChanges() > 0;
            }
        }
    }
}
