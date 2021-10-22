using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurencTime { get; }
        public string Currency { get; }
        public string TypeOfTransaction { get; }
        public string CategoryOfTransaction { get; }
        public decimal Amount { get; }


    }
}