using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TheBTeam.BLL;
using TheBTeam.Web.Models;

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
        
    }
}