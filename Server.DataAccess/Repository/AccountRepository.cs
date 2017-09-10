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
        private readonly DatabaseContext _databaseContext;

        public AccountRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddAccount(string login, string password)
        {
            _databaseContext.Accounts.Add(new Model.Account { Login = login, Password = password });
            _databaseContext.SaveChanges();
        }

        public Guid? GetAccountId(string login, string password)
        {
            return _databaseContext.Accounts.FirstOrDefault(x => x.Login == login && x.Password == password)?.Id;
        }
    }
}
