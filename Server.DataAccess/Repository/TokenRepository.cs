using Server.DataAccess.Context;
using Server.DataAccess.Model;
using System;
using System.Linq;

namespace Server.DataAccess.Repository
{
    public interface ITokenRepository
    {
        bool AddToken(Token token);
        Token GetToken(string refresh_token, string client_id);
        bool ExpireToken(Token token);
    }

    public class TokenRepository : ITokenRepository
    {
        public bool AddToken(Token token)
        {
            using (DBContext db = new DBContext())
            {
                db.Tokens.Add(token);

                return db.SaveChanges() > 0;
            }
        }

        public bool ExpireToken(Token token)
        {
            using (DBContext db = new DBContext())
            {
                db.Tokens.Update(token);

                return db.SaveChanges() > 0;
            }
        }

        public Token GetToken(string refresh_token, string client_id)
        {
            var accountId = new Guid(client_id);
            using (DBContext db = new DBContext())
            {
                return db.Tokens.FirstOrDefault(x => x.AccountId == accountId && x.RefreshToken == refresh_token);
            }
        }
    }
}
