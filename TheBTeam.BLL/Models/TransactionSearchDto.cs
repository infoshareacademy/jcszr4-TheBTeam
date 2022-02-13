using System;
using System.Collections;
using System.Collections.Generic;

namespace TheBTeam.BLL.Models
{
    public class TransactionSearchDto
    {
        public IEnumerable<TransactionDto> Transactions { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public TypeOfTransaction Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }

    }
}