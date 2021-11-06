using TheBTeam.BLL.Model;
using System;
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

        public static IOrderedEnumerable<Transaction> SearchTransactionByDate(TmpDatabase tmpListTransactions, DateTime firstDate, DateTime secondDate, string selectedUserEmail)
        {
            return tmpListTransactions.TransactionsList
                .Where(x => x.OccurenceTime > firstDate).Where(x => x.OccurenceTime < secondDate)
                .Where(x => x.User.Email == selectedUserEmail)
                .OrderBy(x => x.OccurenceTime);
        }
    }
}