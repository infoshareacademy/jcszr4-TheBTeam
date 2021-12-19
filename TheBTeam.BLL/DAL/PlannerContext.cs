using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.DAL
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
    }
}