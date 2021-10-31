using TheBTeam.BLL.Model;
using System;

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
    }
}