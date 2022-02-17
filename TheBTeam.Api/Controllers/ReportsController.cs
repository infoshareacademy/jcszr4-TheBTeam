using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.Models.Reports;

namespace TheBTeam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        // 1. GET'y
        // 2. POST'y
        // 3. PUT/PATCH
        // 4. DELETE

        [HttpGet]
        public async Task<IActionResult> GetSummaryReport()
        {
            return Ok("Wszystko ok");
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(ReportDto report)
        {
            return Ok();
        }


        [HttpPost("log")]
        public async Task<IActionResult> AddLogReport(ReportDto reportLog)
        {
            return Ok("Wszystko ok z innego endpointa");
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteReport(string reportId)
        //{
        //    return Ok();
        //}
    }
}
