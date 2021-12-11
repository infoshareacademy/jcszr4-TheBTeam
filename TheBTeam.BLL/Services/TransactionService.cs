using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private static List<Transaction> Transactions = new List<Transaction>();

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type)
        {
            //var transaction = new Transaction();
            //transaction.Id = GetNextId();
            if (category == CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return Transactions;
            }
            if (category == CategoryOfTransaction.All && type != TypeOfTransaction.All)
            {
                return Transactions.Where(t => t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && (type == TypeOfTransaction.Income || type == TypeOfTransaction.Outcome))
            {
                return Transactions.Where(t => t.Category == category && t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return Transactions.Where(t => t.Category == category).ToList();
            }
            return Transactions;
        }
        public int GetNextId()
        {
            if (!Transactions.Any())
                return 0;
            return (Transactions?.Max(m => m.Id) ?? 0) + 1;
        }

        public Transaction GetById(int id)
        {
            return Transactions.SingleOrDefault(m => m.Id == id);
        }

        public void Create(Transaction model)
        {
            model.Id = GetNextId();
            model.OccurrenceTime = DateTime.Now;
            Transactions.Add(model);
        }

        public void AddTransactionByUser(Transaction model, User user)
        {
            model.Id = GetNextId();
            model.User = user;
            model.OccurrenceTime = DateTime.Now;
            ApplyTransaction(model, user);
            Transactions.Add(model);
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
            return Transactions.Where(t => t.User.Id == id).ToList();
        }
        public static List<Transaction> SearchTransactionByType(TypeOfTransaction type, List<Transaction> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }

        public static void CancelTransaction(Transaction transaction, User user)
        {
            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance -= transaction.Amount;
                transaction.BalanceAfterTransaction = user.Balance;
                return;
            }

            user.Balance += transaction.Amount;
            transaction.BalanceAfterTransaction = user.Balance;
        }

        public static void UpdateTransaction(Transaction transaction,decimal lastAmount, User user)
        {
            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += lastAmount;
                transaction.BalanceAfterTransaction = user.Balance;
                return;
            }
            user.Balance += lastAmount;
            transaction.BalanceAfterTransaction = user.Balance;
        }

        public void Update(Transaction model)
        {
            var transaction = GetById(model.Id);
            transaction.Category = model.Category;
            transaction.Type = model.Type;
            var lastAmount = 0m;
            if (model.Type == TypeOfTransaction.Income)
            {
                 lastAmount = model.Amount - transaction.Amount;
            }
            if (model.Type == TypeOfTransaction.Outcome)
            {
                lastAmount = transaction.Amount - model.Amount;
            }
            transaction.Amount = model.Amount;            
            var user = transaction.User;
            UpdateTransaction(transaction, lastAmount, user);
        }

        public void Delete(int id)
        {
            var transaction = GetById(id);
            var user = transaction.User;
            CancelTransaction(transaction, user);
            Transactions.Remove(transaction);
        }
    }
}