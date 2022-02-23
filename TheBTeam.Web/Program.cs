using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.Services;

namespace TheBTeam.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileLog = new LoggerService();
            fileLog.DeleteLogFile();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var connectionString = "Server=localhost\\sqlexpress; Integrated Security=SSPI; Database=TheBTeam;";
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                        {ColumnName = "Scope", PropertyName = "Scope", DataType = SqlDbType.NVarChar, DataLength = 64},
                }
            };

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions
            {
                 AutoCreateSqlTable = true,
                 TableName = "LogEvents",
            }, columnOptions: columnOptions).MinimumLevel.Warning().Enrich.FromLogContext()
            .CreateLogger();

            Log.Information("App starts ... ");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
