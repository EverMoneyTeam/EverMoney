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
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                var query = from cashFlowsCategories in db.CashflowCategories
                            select new Category
                            {
                                Id = cashFlowsCategories.Id,
                                Name = cashFlowsCategories.Name
                            };

                return query.ToList();
            }
        }
    }
}
