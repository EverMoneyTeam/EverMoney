using Client.DataAccess.Context;
using Client.DataAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace Client.DataAccess.Repository
{
    public class UserRepository
    {
        public List<User> GetAllUsers()
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.ToList();
            }
        }

        public bool AddUser(User user)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }
    }
}
