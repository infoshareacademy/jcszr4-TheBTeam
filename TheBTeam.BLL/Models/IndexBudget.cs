using System.Collections.Generic;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class IndexBudget
    {
        public UserDto User { get; set; }
        public IEnumerable<CategoryBudgetDto> CategoryBudget { get; set; }
    }
}