using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; }
        public Currency Currency { get; }
        public TypeOfTransaction Type { get; }
        public User User { get; }
        /// <summary>
        /// 
        /// </summary>
        public CategoryOfTransaction Category { get; }
        public decimal Amount { get; }
        public Transaction(User user, DateTime occurenceTime, Currency currency, TypeOfTransaction type, CategoryOfTransaction category, decimal amount)
        {
            User = user;
            OccurenceTime = DateTime.Now;
            Currency = currency;
            Amount = amount;
            Category = category;
        }
    }
}