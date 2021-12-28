using System.Collections.Generic;

namespace TheBTeam.BLL.Models
{
    public class UsersBudgetDto
    {
        public IEnumerable<CategoryBudgetDto> UserBudgets { get; set; }
        public int UserId { get; set; }
        public Dictionary<CategoryOfTransaction, decimal> CategorySums { get; set; }
    }
}