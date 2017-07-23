using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Context;
using System;
using Microsoft.Extensions.Logging;

namespace Server.WebAPI.Controller
{
    [Route("api/[controller]")]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SecurityContext _context;
        private readonly ILogger _logger;

        public UserController(SecurityContext context, ILogger<UserController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetByLogin")]
        public IActionResult GetByLogin(string login, string password)
        {
            return Ok(_context.Users.Where(a => a.Account.Login == login && a.Account.Password == password));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_context.Users.Where(a => a.Account.Id == id));
        }
    }
}
