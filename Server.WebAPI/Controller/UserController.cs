using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.DataAccess;
using Server.DataAccess.Context;
using System;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Server.WebAPI.Controller
{
    [Route("api/[controller]")]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SecurityContext _securityContext;
        private readonly ILogger _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _securityContext = ContextFactory.SecurityContext;
        }

        // GET: /api/user/GetByLogin?login=...&password=...  
        [HttpGet]
        public IActionResult Get(string login, string password)
        {
            try
            {
                return Ok(_securityContext.Users.Where(a => a.Account.Login == login && a.Account.Password == password));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
