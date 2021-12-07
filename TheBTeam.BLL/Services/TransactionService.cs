using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private static List<Transaction> _transactions = new List<Transaction>();

        public List<Transaction> GetAll(CategoryOfTransaction category, TypeOfTransaction type)
        {
            if (category == CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return _transactions;
            }
            if (category == CategoryOfTransaction.All && type != TypeOfTransaction.All)
            {
                return _transactions.Where(t => t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && (type == TypeOfTransaction.Income || type == TypeOfTransaction.Outcome))
            {
                return _transactions.Where(t => t.Category == category && t.Type == type).ToList();
            }
            if (category != CategoryOfTransaction.All && type == TypeOfTransaction.All)
            {
                return _transactions.Where(t => t.Category == category).ToList();
            }
            return _transactions;
        }

        public void AddTransactionByUser(Transaction model, User user)
        {
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

        public Transaction GetTransactionByUser(string id)//TODO error if you want delete more than one transaction for the same user! 
        {
            return _transactions.SingleOrDefault(t => t.User.Id == id);
        }
        public Transaction GetTransactionByEmail(string email)
        {
            return _transactions.SingleOrDefault(t => t.User.Email == email);
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

        public User GetUserFromTransaction(Transaction transaction)
        {
            return transaction.User;
        }

        public void Delete(string id)
        {
            var transactionByEmail = GetTransactionByEmail(id);
            var user = GetUserFromTransaction(transactionByEmail);
            CancelTransaction(transactionByEmail, user);
            _transactions.Remove(transactionByEmail);
        }
    }
}