using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using System.Linq;
using System.IO;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private int incomeCategoryLimit = 100;

        private static List<Transaction> _transactions = LoadDataFromFile.ReadAndApplyTransactionFile(UserService._users);

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type, string id = null)
        {
            var transaction = new List<Transaction>();

            if (id == null)
                transaction = _transactions.OrderByDescending(t => t.OccurrenceTime).ToList(); 
            else
                transaction = _transactions.Where(t => t.User.Id == id).OrderByDescending(t => t.OccurrenceTime).ToList();

            if (type == TypeOfTransaction.All)
                return transaction;

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.AllIncome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Income).OrderByDescending(t => t.OccurrenceTime).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Outcome).OrderByDescending(t => t.OccurrenceTime).ToList();

            if (type == TypeOfTransaction.Income && (int)category < incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).OrderByDescending(t => t.OccurrenceTime).ToList();

            if (type == TypeOfTransaction.Outcome && (int)category > incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).OrderByDescending(t => t.OccurrenceTime).ToList();

            throw new InvalidDataException();
        }
        public int GetNextId()
        {
            if (!_transactions.Any())
                return 0;
            return (_transactions?.Max(m => m.Id) ?? 0) + 1;
        }

        public Transaction GetById(int id)
        {
            return _transactions.SingleOrDefault(m => m.Id == id);
        }

        public void Create(Transaction model)
        {
            model.Id = GetNextId();
            model.OccurrenceTime = DateTime.Now;
            _transactions.Add(model);
        }

        public void AddTransactionByUser(Transaction model, User user)
        {
            model.Id = GetNextId();
            model.User = user;
            model.OccurrenceTime = DateTime.Now;
            ApplyTransaction(model, user);
            _transactions.Add(model);
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
            _transactions.Remove(transaction);
        }
    }
}