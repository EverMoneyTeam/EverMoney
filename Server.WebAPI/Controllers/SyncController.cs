using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.DataAccess;
using Server.DataAccess.Repository;
using Server.WebApi.AppSetting;
using Server.WebApi.ExceptionHandler.ValidateModel;
using Server.WebApi.Helper;
using Server.WebApi.ViewModel;

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
        public IActionResult GetSyncState()
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.GetLastUpdateSequenceNumber(accountId);
            var dateNow = DateTime.Now.ToUniversalTime();
            return Json(new { USN = usn, Time = dateNow });
        }

        [HttpGet("SyncChunk")]
        public IActionResult GetSyncChunk(int startAt, int count)
        {
            var accountId = HttpContext.GetAccountId();
            return Json(_historyChangeRepository.GetChanges(accountId, startAt, count).Select(c => new { c.USN, c.Table, c.Column, c.RowId, c.Value }).ToList());
        }

        [HttpPost("CreateCashAccount")]
        public IActionResult CreateCashAccount([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.InsertDataRow(accountId, SyncTable.CashAccount.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(201, new { USN = usn }) : BadRequest("Could not create Cash Account");
        }

        [HttpPost("CreateCashFlow")]
        public IActionResult CreateCashFlow([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.InsertDataRow(accountId, SyncTable.CashFlow.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(201, new { USN = usn }) : BadRequest("Could not create Cash Flow");
        }

        [HttpPost("CreateCashFlowCategory")]
        public IActionResult CreateCashFlowCategory([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.InsertDataRow(accountId, SyncTable.CashFlowCategory.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(201, new { USN = usn }) : BadRequest("Could not create Cash Flow Category");
        }

        [HttpPost("DeleteCashAccount")]
        public IActionResult DeleteCashAccount([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.DeleteDataRow(accountId, SyncTable.CashAccount.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Account");
        }

        [HttpPost("DeleteCashFlow")]
        public IActionResult DeleteCashFlow([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.DeleteDataRow(accountId, SyncTable.CashFlow.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Flow");
        }

        [HttpPost("DeleteCashFlowCategory")]
        public IActionResult DeleteCashFlowCategory([FromBody]SyncSimpleParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.DeleteDataRow(accountId, SyncTable.CashFlowCategory.ToString(), parameters.RowId.ToString());
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Flow Category");
        }

        [HttpPost("UpdateCashAccount")]
        public IActionResult UpdateCashAccount([FromBody]SyncUpdateParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.UpdateDataRow(accountId, SyncTable.CashAccount.ToString(), parameters.RowId.ToString(), parameters.Column, parameters.Value);
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Account");
        }

        [HttpPost("UpdateCashFlow")]
        public IActionResult UpdateCashFlow([FromBody]SyncUpdateParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.UpdateDataRow(accountId, SyncTable.CashFlow.ToString(), parameters.RowId.ToString(), parameters.Column, parameters.Value);
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Flow");
        }

        [HttpPost("UpdateCashFlowCategory")]
        public IActionResult UpdateCashFlowCategory([FromBody]SyncUpdateParameters parameters)
        {
            var accountId = HttpContext.GetAccountId();
            var usn = _historyChangeRepository.UpdateDataRow(accountId, SyncTable.CashFlowCategory.ToString(), parameters.RowId.ToString(), parameters.Column, parameters.Value);
            return usn > 0 ? StatusCode(200, new { USN = usn }) : BadRequest("Could not delete Cash Flow Category");
        }
    }
}
