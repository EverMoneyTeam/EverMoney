using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.DataAccess.Repository;
using Server.WebApi.AppSetting;
using Server.WebApi.ExceptionHandler.ValidateModel;
using Server.WebApi.Helper;

namespace Server.WebApi.Controllers
{
    [Route("api/sync")]
    [ValidateModel]
    [Authorize]
    public class SyncController : Controller
    {
        private readonly IHistoryChangeRepository _historyChangeRepository;

        public SyncController(IHistoryChangeRepository historyChangeRepository)
        {
            _historyChangeRepository = historyChangeRepository;
        }

        [HttpGet("SyncState")]
        [Authorize]
        public IActionResult GetSyncState()
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.GetLastUpdateSequenceNumber(accountId);
            var dateNow = DateTime.Now.ToUniversalTime();
            return Json(new { USN = usn, Time = dateNow });
        }
    }
}
