using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.DataAccess.Context;
using Client.DataAccess.Model;

namespace Client.DataAccess.Repository
{
    public static class AccountRepository
    {
        public static Account GetAccount(string id)
        {
            using (var db = DbContextFactory.GetDbContext())
            {
                return db.Accounts.FirstOrDefault(a => a.Id == id);
            }
        }
    }
}
