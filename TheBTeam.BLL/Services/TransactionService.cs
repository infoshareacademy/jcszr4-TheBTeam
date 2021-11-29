using TheBTeam.BLL.Model;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private List<Transaction> Transaction = new List<Transaction>
        {
            new Transaction
            {
                User = new User("mm@wp.pl"),
                Type = TypeOfTransaction.Income,
                Category = CategoryOfTransaction.Salary,
                Currency = Currency.PLN,
                Amount = 1220m,
                BalanceAfterTransaction = GetBalanceAfterTransaction(TypeOfTransaction.Income,1220m)
            },
            new Transaction
            {
                User = new User("mariobros@wp.pl"),
                Type = TypeOfTransaction.Income,
                Category = CategoryOfTransaction.Salary,
                Currency = Currency.PLN,
                Amount = 1520m,
                BalanceAfterTransaction = GetBalanceAfterTransaction(TypeOfTransaction.Income,1520m)
            },
            new Transaction
            {
                User = new User("grzesio@wp.pl"),
                Type = TypeOfTransaction.Outcome,
                Category = CategoryOfTransaction.Credit,
                Currency = Currency.PLN,
                Amount = 2520m,
                BalanceAfterTransaction = GetBalanceAfterTransaction(TypeOfTransaction.Outcome,2520m)
            },
            new Transaction
            {
                User = new User("edekwielki@wp.pl"),
                Type = TypeOfTransaction.Outcome,
                Category = CategoryOfTransaction.Car,
                Currency = Currency.PLN,
                Amount = 1250m,
                BalanceAfterTransaction = GetBalanceAfterTransaction(TypeOfTransaction.Outcome,1250m)
            }
        };

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type)
        {
            if (category == CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return Transaction;
            }
            if (category == CategoryOfTransaction.All && type != TypeOfTransaction.All)
            {
                return Transaction.Where(t=>t.Type == type).ToList();
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
            modelT.OccurenceTime = DateTime.Now;
            ApplyTransaction(modelT, user);
            Transaction.Add(modelT);
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
        public static List<Transaction> SearchTransactionByUser(User user, List<Transaction> transactions)
        {
            var transactionByUser = transactions.Where(t => t.User == user).ToList();
            return transactionByUser;
        }
        public static List<Transaction> SearchTransactionByType(TypeOfTransaction type, List<Transaction> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }
    }
}