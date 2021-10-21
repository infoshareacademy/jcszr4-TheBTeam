using System;

namespace TheBTeam.BLL.Model
{
    public class Transaction
    {
        public DateTime OccurencTime { get; set; }
        public string Currency { get; }
        public string TypeOfTransaction { get; }
        public CategoryOfTransaction CategoryOfTransaction { get; }
        public decimal Amount { get; }



    }
}