using System;
using System.ComponentModel.DataAnnotations.Schema;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.DAL
{
    public class CategoryBudget
    {

        public int Id { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public decimal PlanedBudget { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        //public DateTime Date { get; set; }

        public static CategoryBudget FromDto(CategoryBudgetDto categoryBudgetDto)
        {
            var categoryBudget = new CategoryBudget()
            {
                Category = categoryBudgetDto.Category,
                Id = categoryBudgetDto.Id,
                PlanedBudget = categoryBudgetDto.PlanedBudget,
                UserId = categoryBudgetDto.UserId
            };
            return categoryBudget;
        }
    }
}