using System;
using TheBTeam.BLL;

namespace TheBTeam.Web.Models
{
    public class CategoryReport
    {
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public DateTime Date { get; set; }
    }
}