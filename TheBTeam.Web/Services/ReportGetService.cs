using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TheBTeam.BLL;
using TheBTeam.Web.Models;
using TheBTeam.BLL.Models.Reports;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace TheBTeam.Web.Services
{
    public class ReportGetService
    {
        private IConfiguration Configuration { get; }

        public async Task<List<ReportCategoryDto>> GetReport()
        {

            using var client = new HttpClient();


            var apiUriBase = Configuration.GetValue<string>("ReportsApiUrl");
            using (HttpResponseMessage response = await client.GetAsync(apiUriBase + "Reports/GetCategoryReport"))
            {
                string stringResponse = response.ToString();

                var Report = JsonConvert.DeserializeObject<List<ReportCategoryDto>>(stringResponse);

                return Report;
            }
        }

    }
}
