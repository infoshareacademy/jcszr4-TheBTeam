using TheBTeam.Api.Enums;

namespace TheBTeam.Api.Models
{
    public class CategoryReport: Entity
    {
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public CategoryOfTransaction Category { get; set; }
    }
}