using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.DataAccess;
using Server.DataAccess.Context;
using Server.DataAccess.Model;

namespace Server.WebAPI.Controller
{
    [Route("api/[controller]")]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SecurityContext _securityContext;

        public UserController()
        {
            _securityContext = ContextFactory.SecurityContext;
        }

        // GET: /api/user?login=...&password=...  
        [HttpGet]
        public IEnumerable<User> Get(string login, string password)
        {
            return _securityContext.Users.Where(a => a.Account.Login == login && a.Account.Password == password);
        }
    }
}
