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
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }

    }
}