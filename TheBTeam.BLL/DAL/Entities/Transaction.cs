using System;
using System.ComponentModel.DataAnnotations;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.DAL.Entities
{
    public class Transaction : Entity
    {
        public Currency Currency { get; set; }
        public TypeOfTransaction Type { get; set; }

        [Required]
        public User User { get; set; }
        public CategoryOfTransaction Category { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$")]
        public decimal Amount { get; set; }

        [DataType(DataType.Currency)]
        public decimal BalanceAfterTransaction { get; set; }

        public static Transaction FromDto(TransactionDto transactionDto)
        {
            return new Transaction
            {
                //CreatedAt = transactionDto.CreatedAt,
                Currency = transactionDto.Currency,
                Type = transactionDto.Type,
                User = User.FromDto(transactionDto.UserDto),
                Category = transactionDto.Category,
                Amount = transactionDto.Amount,
                BalanceAfterTransaction = transactionDto.BalanceAfterTransaction
            };
        }
    }
}
