using TheBTeam.BLL.Model;
using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        public static void ApplyTransaction(Transaction transaction, User user)
        {

            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += transaction.Amount;
                return;
            }

            user.Balance -= transaction.Amount;
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