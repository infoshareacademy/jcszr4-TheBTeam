using System;
using System.ComponentModel.DataAnnotations;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.DAL.Entities
{
    public class Transaction : Entity
    {
        public Currency Currency { get; set; }
        public TypeOfTransaction Type { get; set; }
        public User User { get; set; }
        public CategoryOfTransaction Category { get; set; }

        [Required(ErrorMessage = "Please provide value")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Amount can't be negative")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Provide valid price")]
        public decimal Amount { get; set; }

        [DataType(DataType.Currency)]
        public decimal BalanceAfterTransaction { get; set; }
    }
}
