using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; set; }
        public Currency Currency { get; set; }
        public TypeOfTransaction Type { get; set; }
        public User User { get; }
        public string TransactionTitle { get; set; }
        /// <summary>
        ///
        /// </summary>
        public CategoryOfTransaction Category { get; set; }
        public decimal Amount { get; set; }
        public Transaction(User user, DateTime occurenceTime, Currency currency, TypeOfTransaction type, CategoryOfTransaction category, decimal amount,string transactionTitle)
        {
            User = user;
            OccurenceTime = DateTime.Now;
            Currency = currency;
            Amount = amount;
            Category = category;
            TransactionTitle = transactionTitle;
        }
    }
}