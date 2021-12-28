using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class CategoryBudgetDto
    {
        public int Id { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public decimal PlanedBudget { get; set; }
        public int UserId { get; set; }

        public static CategoryBudgetDto FromDal(CategoryBudget categoryBudget)
        {
            var categoryBudgetDto = new CategoryBudgetDto()
            {
                Category = categoryBudget.Category,
                Id = categoryBudget.Id,
                PlanedBudget = categoryBudget.PlanedBudget,
                UserId = categoryBudget.UserId
            };
            return categoryBudgetDto;
        }
    }
}