using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private int incomeCategoryLimit = 99;

        private static List<Transaction> _transaction = new List<Transaction>();
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
            if (type == TypeOfTransaction.All)
                return _transaction;

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.AllIncome)
                return _transaction.Where(t => t.Type == TypeOfTransaction.Income).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return _transaction.Where(t => t.Type == TypeOfTransaction.Outcome).ToList();

            if (type == TypeOfTransaction.Income && (int)category <= incomeCategoryLimit)
                return _transaction.Where(t => t.Category == category).ToList();

            if (type == TypeOfTransaction.Outcome && (int) category > incomeCategoryLimit)
                return _transaction.Where(t => t.Category == category).ToList();


            //todo  add more options, check if it is seal no erros can be generated!
                if (category != CategoryOfTransaction.All && (type == TypeOfTransaction.Income || type == TypeOfTransaction.Outcome))
                {
                    return _transaction.Where(t => t.Category == category && t.Type == type).ToList();
                }
            if (category != CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return _transaction.Where(t => t.Category == category).ToList();
            }
            return _transaction;
        }

        public List<Transaction> AddTransaction(Transaction modelT, User user)
        {
            modelT.OccurrenceTime = DateTime.Now;
            modelT.User = user;
            ApplyTransaction(modelT, user);
            _transaction.Add(modelT);
            return _transaction;
        }

        public List<Transaction> GetTransactionFromUser()
        {
            return _transaction;
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
            return _transaction.Where(t => t.User.Id == id).ToList();
        }
        public static List<Transaction> SearchTransactionByType(TypeOfTransaction type, List<Transaction> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }
    }
}