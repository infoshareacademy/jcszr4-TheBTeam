using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Models.Reports;

namespace TheBTeam.BLL.DAL
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryBudget> CategoryBudgets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ReportCategoryDto> Report { get; set; }
        public DbSet<AdminPanelDto> AdminPanelDtos { get; set; }
    }
}