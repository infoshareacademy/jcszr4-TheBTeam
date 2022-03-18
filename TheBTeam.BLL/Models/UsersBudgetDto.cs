using System;
using System.Collections.Generic;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class UsersBudgetDto
    {
        public IEnumerable<CategoryBudgetDto> UserBudgets { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public Dictionary<CategoryOfTransaction, decimal> CategorySums { get; set; }
        public List<Tuple<string, decimal>> Expenses { get; set; }
        public DateTime Date { get; set; }
    }
}