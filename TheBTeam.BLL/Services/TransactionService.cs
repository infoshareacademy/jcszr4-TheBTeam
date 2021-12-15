using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private int incomeCategoryLimit = 100;

        private static readonly List<Transaction> _transactions = new List<Transaction>();
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

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type, string id= null)
        {
            var transaction = new List<Transaction>();

            if (id == null)
                transaction = _transactions;
            else
                transaction=_transactions.Where(t => t.User.Id == id).ToList();
            
            if (type == TypeOfTransaction.All)
                return transaction;

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.AllIncome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Income).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Outcome).ToList();

            if (type == TypeOfTransaction.Income && (int)category < incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).ToList();

            if (type == TypeOfTransaction.Outcome && (int)category > incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).ToList();

            throw new InvalidDataException();

        }

        public List<Transaction> AddTransaction(Transaction modelT, User user)
        {
            modelT.OccurrenceTime = DateTime.Now;
            modelT.User = user;
            ApplyTransaction(modelT, user);
            _transactions.Add(modelT);
            return _transactions;
        }

        public List<Transaction> GetTransactionFromUser()
        {
            return _transactions;
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
            return _transactions.Where(t => t.User.Id == id).ToList();
        }
        public static List<Transaction> SearchTransactionByType(TypeOfTransaction type, List<Transaction> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }
    }
}