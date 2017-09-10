using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Context;
using Microsoft.AspNetCore.Authorization;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetByLogin")]
        public IActionResult GetByLogin(string login, string password)
        {
            return Ok(_context.Users.Where(a => a.Account.Login == login && a.Account.Password == password));
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public IActionResult GetById(Guid id)
        {
            return Ok(_context.Users.Where(a => a.Account.Id == id));
        }
    }

}
