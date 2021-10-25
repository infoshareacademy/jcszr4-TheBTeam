using System;
using System.Collections.Generic;
using System.Text;
using TheBTeam.BLL.Model;

namespace TheBTeam.ConsoleApp
{
    public class TransactionViewer
    {
        public string ViewTransaction(List<Transaction> transactions)
        {
            const int lenghtFirstName = -13;
            const int lenghtLastName = -13;
            const int lenghtOccuranceTime = -22;
            const int lenghtAmount = -15;
            const int lenghtTypeCategory = -14;
            const int lenghtCategoryOfTransaction = -25;
            const int lenghtCurrency = -10;
            var report = new StringBuilder();
            report.AppendLine($"|{"FirstName",lenghtFirstName}|{"LastName",lenghtLastName}|" +
                   $"{"OccuranceTime",lenghtOccuranceTime}|{"Amount",lenghtAmount}|{ "Currency",lenghtCurrency}| " + 
                   $"{"TypeCategory",lenghtTypeCategory}|{"CategoryOfTransaction",lenghtCategoryOfTransaction}|");
            report.AppendLine(new String('-', 120));
            foreach (var item in transactions)
            {
                if (item.User != null)
                {
                    report.AppendLine($"|{item.User.FirstName,lenghtFirstName}|" +
                                      $"{item.User.LastName,lenghtLastName}|" +
                                      $"{item.OccurenceTime,lenghtOccuranceTime}|" +
                                      $"{item.Amount,lenghtAmount}|" +
                                      $"{item.Currency,lenghtCurrency}|" +
                                      $"{item.Type,lenghtTypeCategory}|" +
                                      $"{item.Category,lenghtCategoryOfTransaction}|");
                }
            }
            return report.ToString();
        }
    }
}
