using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;

namespace TheBTeam.ConsoleApp
{
    public static class TransactionViewer
    {
        public static void ViewTransaction(List<Transaction> transactions)
        {
            var textPaddingWidth = 23;
            var paddingChar = ' ';
            Console.WriteLine($"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"OccuranceTime".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
            foreach (var item in transactions)
            {
                if (item.User != null)
                {
                    Console.WriteLine($"|{item.User.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{item.User.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{item.OccurenceTime.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)}");
                }
            }
        }
        public static void ViewTransactionAccordingCategory(List<Transaction> transactions)
        {
            //TODO : added here readline with user!
            var email = ConsoleFactory.GetEmail();
            var categoryOfTransaction = ConsoleFactory.GetCategoryOfTransaction();
            Console.WriteLine($"{categoryOfTransaction}");
            var tmpTransactions = transactions.Where(t => t.User.Email == email).Where(t => t.Category == categoryOfTransaction);
            var textPaddingWidth = 23;
            var paddingChar = ' ';
            Console.WriteLine($"|{"Category".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"OccuranceTime".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
            foreach (var item in tmpTransactions)
            {
                if (item.User != null)
                {
                    Console.WriteLine($"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.OccurenceTime.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
                }
            }
        }
    }
}
