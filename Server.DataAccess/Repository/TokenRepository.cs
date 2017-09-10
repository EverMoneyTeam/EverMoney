using Server.DataAccess.Context;
using Server.DataAccess.Model;
using System;
using System.Linq;

namespace Server.DataAccess.Repository
{
    public interface ITokenRepository
    {
        bool AddToken(Token token);
        Token GetToken(string refreshToken, string accountId);
        bool ExpireToken(Token token);
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TokenRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddToken(Token token)
        {
            _databaseContext.Tokens.Add(token);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool ExpireToken(Token token)
        {
            token.IsStop = 1;
            _databaseContext.Tokens.Update(token);
            return _databaseContext.SaveChanges() > 0;
        }

        public Token GetToken(string refreshToken, string accountId)
        {
            var accountIdGuid = new Guid(accountId);
            return _databaseContext.Tokens.FirstOrDefault(x => x.AccountId == accountIdGuid && x.RefreshToken == refreshToken);
        }
    }
}
