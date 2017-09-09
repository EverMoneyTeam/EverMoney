using Server.DataAccess.Context;
using System;
using System.Linq;

namespace Server.DataAccess.Repository
{
    public interface IAccountRepository
    {
        Guid? GetAccountId(string login, string password);
        void AddAccount(string login, string password);
    }

    public class AccountRepository : IAccountRepository
    {
        public void AddAccount(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                db.Accounts.Add(new Model.Account { Login = login, Password = password });
                db.SaveChanges();
            }
        }

        public Guid? GetAccountId(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                var account = db.Accounts.FirstOrDefault(x => x.Login == login
                                        && x.Password == password);
                if (account == null) return null;
                else return account.Id;

            }
        }


    }
}
