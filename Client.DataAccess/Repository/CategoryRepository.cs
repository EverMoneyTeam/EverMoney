using Client.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataAccess.Repository
{
    public class Category
    {
        public string Name { get; set; }
    }
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            using (var db = new DatabaseContext())
            {
                var query = from cashFlowsCategories in db.CashflowCategories
                        select new Category
                            {
                                Name = cashFlowsCategories.Name
                            };

                return query.ToList();
            }
        }
    }
}
