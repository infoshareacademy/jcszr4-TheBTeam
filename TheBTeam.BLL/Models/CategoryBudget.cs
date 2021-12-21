using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class CategoryBudget
    {
        public int Id { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public decimal PlanedBudget { get; set; }
        public int UserId { get; set; }

    }
}