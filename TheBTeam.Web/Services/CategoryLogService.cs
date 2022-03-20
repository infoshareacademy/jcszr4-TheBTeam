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
    public class CategoryLogService
    {
       
            private IConfiguration Configuration { get; }

            public CategoryLogService(IConfiguration configuration)
            {
                Configuration=configuration;
            }
            public async Task ReportOutcomeCategory(decimal amount, int userId, CategoryOfTransaction category, DateTime date)
            {
                using var client = new HttpClient();

                var report = new CategoryReport
                {
                    Amount = amount,
                    UserID = userId,
                    Category = category,
                    Date = date
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(report);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var apiUriBase = Configuration.GetValue<string>("ReportsApiUrl");

                await client.PostAsync(apiUriBase + "Reports/AddTransaction", httpContent);
            }
            public async Task LogInOutcome(int userId, DateTime date)
            {
                using var client = new HttpClient();

                var report = new UserLoginResult
                {
                    ActivityDate = date,
                    UserID = userId
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(report);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var apiUriBase = Configuration.GetValue<string>("ReportsApiUrl");

                await client.PostAsync(apiUriBase + "Reports/AddUserActivityReport", httpContent);
            }
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