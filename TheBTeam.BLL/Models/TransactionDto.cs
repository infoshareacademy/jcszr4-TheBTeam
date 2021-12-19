using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace TheBTeam.BLL.Models
{
    public class TransactionDto
    {
        [Display(Name = "Occurrence Time")]
        public DateTime OccurrenceTime { get; set; }
        public Currency Currency { get; set; }

        [Display(Name = "Type")]
        public TypeOfTransaction Type { get; set; }
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
        
        /*public Transaction(User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {         
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            OccurrenceTime = DateAndTime.Now;
        }*/
        public TransactionDto()
        {
            OccurrenceTime = DateTime.Now;
        }
    }
}