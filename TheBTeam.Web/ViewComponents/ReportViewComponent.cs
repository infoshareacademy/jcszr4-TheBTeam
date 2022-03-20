using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.Models.Reports;
using TheBTeam.Web.Services;

namespace TheBTeam.Web
{
    public class ReportViewComponent : ViewComponent
    {
        CategoryLogService _categoryLogService;
        
        private IConfiguration _configuration { get; }

        public async Task<List<ReportCategoryDto>> GetReportCategoryDtos()
        {
            List<ReportCategoryDto> _deserializedReport = await _categoryLogService.GetReport();
            return await Task.FromResult(new List<ReportCategoryDto>());
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = GetReportCategoryDtos();
            return View(model);
        }
    }
}
