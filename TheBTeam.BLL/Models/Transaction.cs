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
        public CategoryOfTransaction Category { get; set; }
        public decimal Amount { get; }
        public decimal BalanceAfterTransaction { get; set; }
        public Transaction(User user, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {
            OccurenceTime = DateTime.Now;
            User = user;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            //BalanceAfterTransaction=
        }
        
    }
    public class TransactionTest
    {
        public string Email { get; set; }
        public DateTime OccurenceTime { get; }
        public Currency Currency { get; }
        public TypeOfTransaction Type { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public decimal Amount { get; }
        public decimal BalanceAfterTransaction { get; set; }
        public TransactionTest(string email, TypeOfTransaction type, CategoryOfTransaction category, Currency currency, decimal amount)
        {
            OccurenceTime = DateTime.Now;
            Type = type;
            Category = category;
            Currency = currency;
            Amount = amount;
            //BalanceAfterTransaction=
        }
    }
}