using System.Collections;
using System.Collections.Generic;

namespace TheBTeam.BLL.Models
{
    public class UsersTrendingDto
    {
        public IEnumerable Transactions { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public int UserId { get; set; }
    }
}