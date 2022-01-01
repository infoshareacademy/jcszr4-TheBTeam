using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class TransactionDto
    {
        [Display(Name = "Occurrence Time")]
        public DateTime CreatedAt { get; set; }
        public Currency Currency { get; set; }
        public int Id { get; set; }

        [Display(Name = "Type")]
        public TypeOfTransaction Type { get; set; }

        public int? UserId { get; set; }
        public UserDto UserDto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Category")]
        public CategoryOfTransaction Category { get; set; }

        [Required(ErrorMessage = "Please provide value")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Amount can't be negative")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Provide valid price")]
        public decimal Amount { get; set; }

        [Display(Name = "Balance after transaction")]
        [DataType(DataType.Currency)]
        public decimal BalanceAfterTransaction { get; set; }

        public string Description { get; set; }//TODO: add limitation to 50 chars

        public static TransactionDto FromDal(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                CreatedAt = transaction.CreatedAt.ToLocalTime(),
                Currency = transaction.Currency,
                Type = transaction.Type,
                UserDto = UserDto.FromDAL(transaction.User),
                Category = transaction.Category,
                Amount = transaction.Amount,
                BalanceAfterTransaction = transaction.BalanceAfterTransaction,
               Description = transaction.Title
            };
        }
    }
}