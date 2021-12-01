using TheBTeam.BLL.Model;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private static List<Transaction> Transaction = new List<Transaction>();
        //private list<transaction> transaction = new list<transaction>
        //{
        //    new transaction
        //    {
        //        user = new user("mm@wp.pl"),
        //        type = typeoftransaction.income,
        //        category = categoryoftransaction.salary,
        //        currency = currency.pln,
        //        amount = 1220m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.income,1220m)
        //    },
        //    new transaction
        //    {
        //        user = new user("mariobros@wp.pl"),
        //        type = typeoftransaction.income,
        //        category = categoryoftransaction.salary,
        //        currency = currency.pln,
        //        amount = 1520m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.income,1520m)
        //    },
        //    new transaction
        //    {
        //        user = new user("grzesio@wp.pl"),
        //        type = typeoftransaction.outcome,
        //        category = categoryoftransaction.credit,
        //        currency = currency.pln,
        //        amount = 2520m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.outcome,2520m)
        //    },
        //    new transaction
        //    {
        //        user = new user("edekwielki@wp.pl"),
        //        type = typeoftransaction.outcome,
        //        category = categoryoftransaction.car,
        //        currency = currency.pln,
        //        amount = 1250m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.outcome,1250m)
        //    }
        //};

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type)
        {
            if (category == CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return Transaction;
            }
            if (category == CategoryOfTransaction.All && type != TypeOfTransaction.All)
            {
                return Transaction.Where(t => t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && (type == TypeOfTransaction.Income || type == TypeOfTransaction.Outcome))
            {
                return Transaction.Where(t => t.Category == category && t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return Transaction.Where(t => t.Category == category).ToList();
            }
            return Transaction;
        }

        public List<Transaction> AddTransaction(Transaction modelT, User user)
        {
            modelT.OccurrenceTime = DateTime.Now;
            modelT.User = user;
            ApplyTransaction(modelT, user);
            Transaction.Add(modelT);
            return Transaction;
        }

        public List<Transaction> GetTransactionFromUser()
        {
            return Transaction;
        }


        public static decimal GetBalanceAfterTransaction(TypeOfTransaction typeOfTransaction, decimal amount)
        {
            decimal result = 0;
            if (typeOfTransaction == TypeOfTransaction.Income)
            {
                result += amount;
                return result;
            }
            result -= amount;
            return result;
        }

        public static void ApplyTransaction(Transaction transaction, User user)
        {
            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += transaction.Amount;
                transaction.BalanceAfterTransaction = user.Balance;
                return;
            }

            user.Balance -= transaction.Amount;
            transaction.BalanceAfterTransaction = user.Balance;
        }
        public List<Transaction> SearchTransactionByUser(string id)
        {
            return Transaction.Where(t => t.User.Id == id).ToList();
        }
        public static List<Transaction> SearchTransactionByType(TypeOfTransaction type, List<Transaction> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }
    }
}