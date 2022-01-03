using System.Collections.Generic;

namespace TheBTeam.BLL.Models
{
    public class UserTransactionsDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}