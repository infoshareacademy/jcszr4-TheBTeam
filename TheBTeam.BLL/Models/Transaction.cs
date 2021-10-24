using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; set; }
        public Currency Currency { get; }
        public TypeOfTransaction Type { get; }
        //TODO should be in user or separated list?

        /// <summary>
        /// 
        /// </summary>
        public CategoryOfTransaction Category { get; }

        public decimal Amount { get; }
        //TODO to whom
    }
}