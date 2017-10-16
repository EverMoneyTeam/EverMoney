using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class CashFlowCategoryRepository
    {
        public List<CashFlowCategory> GetAllCashFlowCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.ToList();
            }
        }

        public CashFlowCategory GetCashFlowCategoryByName(string name)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.CashFlowCategories.FirstOrDefault(x => x.Name == name);
            }
        }
    }
}
