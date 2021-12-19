namespace TheBTeam.BLL.Models
{
    public class CategoryBudget
    {
        public decimal BudgetLeft;
        public CategoryOfTransaction Category { get; set; }
        public decimal PlanedBudget { get; set; }
        public decimal ActualBudget { get; set; }


    }
}