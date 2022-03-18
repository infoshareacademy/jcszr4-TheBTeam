using Microsoft.EntityFrameworkCore;
using TheBTeam.Api.Models;

namespace TheBTeam.Api.Data
{
    public class ReportsContext : DbContext
    {
        public ReportsContext(DbContextOptions<ReportsContext> options): base(options)
        {
            
        }

        public DbSet<CategoryReport> CategoryReport { get; set; }

        public DbSet<UserLoginResult> UserLoginResult { get; set; }
    }
}