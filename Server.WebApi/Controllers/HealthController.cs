using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.WebApi.Helper;

namespace Server.WebApi.Controllers
{
    [Route("api/health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("authorized")]
        [Authorize]
        public IActionResult GetAuthorized()
        {
            return Json(new {AccountId = HttpContext.GetAccountId()});
        }
    }
}
