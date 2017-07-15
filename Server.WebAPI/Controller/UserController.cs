using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Context;
using System;
using System.Net;
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
            try
            {
                return Ok(_context.Users.Where(a => a.Account.Login == login && a.Account.Password == password));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_context.Users.Where(a => a.Account.Id == id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
