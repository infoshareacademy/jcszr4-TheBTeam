using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurenceTime { get; set; }
        public string Currency { get; }
        public string Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public CategoryOfTransaction Category { get; }

        public decimal Amount { get; }
        //TODO to whome
    }
}