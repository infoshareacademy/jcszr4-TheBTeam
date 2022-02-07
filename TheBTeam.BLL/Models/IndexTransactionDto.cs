using System;
using System.Collections;
using System.Collections.Generic;

namespace TheBTeam.BLL.Models
{
    public class IndexTransactionDto
    {
        public IEnumerable<TransactionDto> Transactions { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public TypeOfTransaction Type { get; set; }

        //TODO:implement it!
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Description { get; set; }

    }
}