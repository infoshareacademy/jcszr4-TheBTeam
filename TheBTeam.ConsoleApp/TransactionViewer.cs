using System.Collections.Generic;
using System.Text;
using TheBTeam.BLL.Model;

namespace TheBTeam.ConsoleApp
{
    public class TransactionViewer
    {
        public string ViewTransaction(List<Transaction> transactions)
        {
            const int lenghtFirstName = -20;
            const int lenghtLastName = -15;
            var report = new StringBuilder();
            foreach (var item in transactions)
            {
                if (item.User != null)
                {
                    report.AppendLine($"|{item.User.LastName,lenghtFirstName}|" +
                                      $"{item.Amount,lenghtFirstName}|" +
                                      $"{item.Category,lenghtLastName}|");
                }
            }
            return report.ToString();
        }
    }
}
