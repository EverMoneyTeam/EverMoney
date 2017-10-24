using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Client.DataAccess.Context;
using Client.DataAccess.Model;

namespace Client.DataAccess.Repository
{
    public static class CurrencyRepository
    {
        public static List<Currency> GetAllCurrencies()
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.Currencies.ToList();
            }
        }
    }
}