using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; }
        public Currency Currency { get; }
        public TypeOfTransaction Type { get; set; }
        public User User { get; }
        /// <summary>
        /// 
        /// </summary>
        public CategoryOfTransaction Category { get; }
        public decimal Amount { get; }
        public Transaction(User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {
            OccurenceTime = DateTime.Now;
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
        }

    }
}