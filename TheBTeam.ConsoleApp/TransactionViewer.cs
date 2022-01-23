using System;
using System.Collections.Generic;
using System.Linq;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;

namespace TheBTeam.ConsoleApp
{
    public static class TransactionViewer
    {
        public static void ViewTransaction(List<Transaction> transactions)
        {
            if (transactions.Count != 0)
            {
                var textPaddingWidth = 20;
                var paddingChar = ' ';
                var numberOfCollumn = 7;
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                Console.WriteLine($"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Currency".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Balance".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                foreach (var item in transactions)
                {
                    Console.WriteLine($"|{item.User.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{item.User.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.Currency.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.BalanceAfterTransaction.ToString("C").PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
                }
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                Console.WriteLine($"Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"No transactions, Please enter transaction");
                Console.WriteLine($"Press any key to continue");
                Console.ReadKey();
            }
        }
        public static void ViewTransactionAccordingCategory(List<Transaction> transactions)
        {
            if (transactions.Count != 0)
            {
                var categoryOfTransaction = ConsoleFactory.GetCategoryOfTransaction();
                Console.WriteLine($"{categoryOfTransaction}");
                var tmpTransactions = transactions.Where(t => t.Category == categoryOfTransaction);
                var textPaddingWidth = 20;
                var paddingChar = ' ';
                var numberOfCollumn = 5;
                if (tmpTransactions.ToList().Count > 0)
                {
                    Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                    Console.WriteLine($"|{"Category".PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{"Type".PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{"OccuranceTime".PadRight(textPaddingWidth, paddingChar)} " +
                                      $"|{"Balance".PadRight(textPaddingWidth, paddingChar)}" +
                                      $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
                    Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                    foreach (var item in tmpTransactions)
                    {
                        Console.WriteLine($"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.OccurrenceTime.ToString("dd/MM/yyyy").PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.User.Balance.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
                    }
                }
                else
                {
                    Console.WriteLine($"No transactions, select another category again");
                }
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                Console.WriteLine($"Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"No transactions, Please enter transaction");
                Console.WriteLine($"Press any key to continue");
                Console.ReadKey();
            }
        }
        public static int ViewTransactionEdit(List<Transaction> transactions)
        {

            int index = 1;
            var textPaddingWidth = 20;
            var paddingChar = ' ';
            var numberOfCollumn = 7;
            Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
            Console.WriteLine(    $"|{"Index".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Currency".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
            foreach (var item in transactions)
            {
                Console.WriteLine($"|{index.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.User.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.User.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Currency.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
                index++;
            }
            Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
            Console.WriteLine("\n\nChoose transaction to edit by index, or type exit to exit\n");
            var name = "Index";
            var min = 1;
            var max = index;
            while (true)
            {
                Console.Write($"{name}: ");
                var input = Console.ReadLine();
                if (input.ToUpper().Trim() == "EXIT")
                {
                    return -1;
                }
                var isDig = int.TryParse(input, out var result);
                if (isDig && result >= min && result < max)
                {
                    return result-1;
                }
                else
                {
                    Console.WriteLine($"{name} should be between {min} and {max - 1}");
                }
            }
        }
        public static void UserAndTypeTransaction(List<User> usersList, List<Transaction> transactions)
        {
            ConsoleFactory.Header("SEARCH USERS TRANSACTION BY TYPE");
            var user = ConsoleFactory.SelectUser(usersList);
            if (user == null)
                return;

            var type = ConsoleFactory.GetTypeOfTransaction();

            var transactionByUser = TransactionService.SearchTransactionByUser(user, transactions);
            var transactionByType = TransactionService.SearchTransactionByType(type, transactionByUser);

            ViewTransaction(transactionByType);
        }

        public static void UserTransaction(List<User> usersList, List<Transaction> transactions)
        {
            ConsoleFactory.Header("SEARCH USERS TRANSACTION BY TYPE");
            var user = ConsoleFactory.SelectUser(usersList);
            if (user == null)
                return;
            var transactionByUser = TransactionService.SearchTransactionByUser(user, transactions);

            ViewTransaction(transactionByUser);
        }

    }
}
