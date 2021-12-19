using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace TheBTeam.BLL.Models
{
    public class Transaction
    {
        //private int StartId = 0;
        //public int Id
        //{
        //    get { return Id; }
        //    set { Id = StartId + 1; }
        //}

        public int Id { get; set; } 

        [Display(Name = "Occurrence Time")]
        public DateTime OccurrenceTime { get; set; }
        public Currency Currency { get; set; }

        [Display(Name = "Type")]
        public TypeOfTransaction Type { get; set; }
        public User User { get; set; }
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

        public Transaction(User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount,int id)
        {
            Id = id;
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            OccurrenceTime = DateAndTime.Now;
        }
        public Transaction(DateTime occurenceTime, User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount,int id)
        {
            OccurrenceTime = occurenceTime;
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            Id = id;
            //BalanceAfterTransaction=
        }
        public Transaction()
        {
            OccurrenceTime = DateTime.Now;
        }
        public string GenerateId()
        {
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
    public class TransactionTest
    {
        public string Id { get; }
        public string Email { get; set; }
        public DateTime OccurenceTime { get; }
        public Currency Currency { get; }
        public TypeOfTransaction Type { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public decimal Amount { get; }
        public decimal BalanceAfterTransaction { get; set; }
        public TransactionTest(string email, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {;
            Email = email;
            OccurenceTime = DateTime.Now;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            //BalanceAfterTransaction=
        }

        [JsonConstructor]
        public TransactionTest(DateTime occurenceTime, string email, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {
            Email = email;
            OccurenceTime = occurenceTime;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            //BalanceAfterTransaction=
        }
        public string GenerateId()
        {
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}