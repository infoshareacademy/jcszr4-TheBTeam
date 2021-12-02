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
            var addNewTransaction = GetTransactionWithUser(modelT, user);
            Transaction.Add(addNewTransaction);
            return Transaction;
        }

        public static Transaction GetTransactionWithUser(Transaction transaction, User user)
        {
            transaction.User = user;
            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += transaction.Amount;
                transaction.BalanceAfterTransaction = user.Balance;
                return transaction;
            }

            user.Balance -= transaction.Amount;
            transaction.BalanceAfterTransaction = user.Balance;
            return transaction;
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