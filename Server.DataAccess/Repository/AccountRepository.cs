using Server.DataAccess.Context;
using System;
using System.Linq;

namespace Server.DataAccess.Repository
{
    public interface IAccountRepository
    {
        Guid GetAccountId(string login, string password);
    }

    public class AccountRepository : IAccountRepository
    {
        public Guid GetAccountId(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                return db.Accounts.FirstOrDefault(x => x.Login == login
                                        && x.Password == password).Id;
            }
        }
    }
}
