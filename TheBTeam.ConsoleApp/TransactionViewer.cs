﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Currency".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
            foreach (var item in transactions)
            {
                Console.WriteLine($"|{item.User.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.User.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Currency.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
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
                Console.WriteLine($"|{item.Category.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.Type.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.OccurenceTime.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                              $"|{item.Amount.ToString().PadRight(textPaddingWidth, paddingChar)}");
            }
        }
        public static int ViewTransactionEdit(List<Transaction> transactions)
        {
            int index = 1;
            var textPaddingWidth = 23;
            var paddingChar = ' ';
            Console.WriteLine(    $"|{"Index".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Type".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Category".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Currency".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Amount".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
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
            Console.WriteLine("\n\nChoose transaction to edit by index, or type 'Exit' to exit\n");
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
                    Console.WriteLine($"{name} should be between {min} and {max-1}");
                }
            }
        }
    }
}
