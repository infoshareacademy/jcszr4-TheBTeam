using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;

namespace TheBTeam.ConsoleApp
{
    public class TransactionViewer
    {
        public void ViewTransaction(List<Transaction> transactions)
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
        public void ViewTransactionAccordingCategory(List<Transaction> transactions)
        {
            var categoryOfTransaction = GetCategoryOfTransaction();
            Console.WriteLine($"{categoryOfTransaction}");
            var tmpTransactions = transactions.Where(t => t.Category == categoryOfTransaction);
            var textPaddingWidth = 23;
            var paddingChar = ' ';
            Console.WriteLine($"|{"Category".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"OccuranceTime".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
            foreach (var item in tmpTransactions)
            {
                Console.WriteLine($"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.OccurenceTime.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
            }
        }
        private static CategoryOfTransaction GetCategoryOfTransaction()
        {
            var currenciesArray = Enum.GetNames(typeof(CategoryOfTransaction));

            Console.WriteLine("Choose your category of transaction:");
            for (int i = 0; i < currenciesArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {currenciesArray[i]}");
            }
            while (true)
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                if (!char.IsDigit(input.KeyChar))
                {
                    Console.WriteLine("Wrong value, try again!\n");
                    continue;
                }

                var isParsed = int.TryParse(input.KeyChar.ToString(), out var selection);

                if (isParsed && selection < currenciesArray.Length)
                    return (CategoryOfTransaction)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }

    }
}
