using System;
using System.Collections.Generic;
using System.Linq;
using TheBTeam.BLL.Model;

namespace TheBTeam.ConsoleApp
{
    public static class TransactionViewer
    {
        public static void ViewTransaction(List<Transaction> transactions)
        {
            var textPaddingWidth = 20;
            var paddingChar = ' ';
            Console.WriteLine(("").PadRight(textPaddingWidth * 7, '='));
            Console.WriteLine($"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Currency".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Balance".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine(("").PadRight(textPaddingWidth * 7, '='));
            foreach (var item in transactions)
            {
                Console.WriteLine($"|{item.User.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.User.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Currency.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.User.Balance.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
            }
            Console.WriteLine(("").PadRight(textPaddingWidth * 7, '='));
            Console.WriteLine($"Press any key to continue");
        }
        public static void ViewTransactionAccordingCategory(List<Transaction> transactions)
        {
            //TODO : added here readline with user!
            var email = ConsoleFactory.GetEmail();
            var categoryOfTransaction = ConsoleFactory.GetCategoryOfTransaction();
            Console.WriteLine($"{categoryOfTransaction}");
            var tmpTransactions = transactions.Where(t => t.User.Email == email).Where(t => t.Category == categoryOfTransaction);
            var textPaddingWidth = 20;
            var paddingChar = ' ';
            Console.WriteLine(("").PadRight(textPaddingWidth * 5, '='));
            Console.WriteLine($"|{"Category".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"OccuranceTime".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Balance".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine(("").PadRight(textPaddingWidth * 5, '='));
            foreach (var item in tmpTransactions)
            {
                Console.WriteLine($"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.OccurenceTime.ToString("dd/MM/yyyy").PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.User.Balance.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                              $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
            }
            Console.WriteLine(("").PadRight(textPaddingWidth * 5, '='));
            Console.WriteLine($"Press any key to continue");
        }
    }
}
