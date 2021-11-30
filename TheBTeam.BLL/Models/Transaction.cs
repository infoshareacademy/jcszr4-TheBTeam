using System;
using Microsoft.VisualBasic;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; set; }
        public Currency Currency { get; set; }
        public TypeOfTransaction Type { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CategoryOfTransaction Category { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        
        public Transaction(User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {         
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            OccurenceTime = DateAndTime.Now;
        }
        public Transaction()
        {
            OccurenceTime = DateTime.Now;
        }
    }
}