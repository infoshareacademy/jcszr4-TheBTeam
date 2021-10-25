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
        
    }
}