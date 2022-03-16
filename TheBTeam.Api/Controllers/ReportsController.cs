using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.Api.Data;
using TheBTeam.Api.Models;
using TheBTeam.BLL.Models.Reports;

namespace TheBTeam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsContext _reportsContext;
        // 1. GET'y
        // 2. POST'y
        // 3. PUT/PATCH
        // 4. DELETE

        public ReportsController(ReportsContext reportsContext)
        {
            _reportsContext = reportsContext;
        }

        [HttpGet("GetMonthlyCategoryReport/{date}")]
        public async Task<IActionResult> GetMonthlyCategoryReport(DateTime date)
        {
            var transactions=_reportsContext.CategoryReport.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year);
            
            var all = transactions.Sum(x => x.Amount); // zsuomwane wartości z kategorii / all
            var finalValues = transactions.GroupBy(x => x.Category).Select(x => new {Category = x.Key, Value = x.Sum(y => y.Amount)/all * 100 });

            return Ok(finalValues);
        }

        [HttpGet("GetCategoryReport")]
        public async Task<IActionResult> GetCategoryReport()
        {
            var transactions = _reportsContext.CategoryReport;
            var all = transactions.Sum(x => x.Amount); // zsuomwane wartości z kategorii / all
            var finalValues = transactions.GroupBy(x => x.Category).Select(x => new { Category = x.Key, Value = x.Sum(y => y.Amount) / all * 100 });

            return Ok(finalValues);
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

        [HttpPost("AddTransaction")]
        public async Task<IActionResult> AddTransaction(CategoryReport report)
        {
            await _reportsContext.AddAsync(report);
            //_reportsContext.Add(report);
            await _reportsContext.SaveChangesAsync();
            return Ok();
        }


    }
}
