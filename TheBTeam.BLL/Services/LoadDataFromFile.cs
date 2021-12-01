using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL.Services

{
    public class LoadDataFromFile
    {
        public static List<User> ReadUserFile()
        {
            string fileName = @"SourceFiles\users.json";

            string jsonString = File.ReadAllText(fileName);
            List<User> userData = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return userData;
        }
        public static List<TransactionTest> ReadTransactionFile(string fileName)
        {
            fileName = "Transactions.json";
            string jsonString = File.ReadAllText(fileName);
            List<TransactionTest> transactionTestData = JsonConvert.DeserializeObject<List<TransactionTest>>(jsonString);
            return transactionTestData;
        }
        public static List<Transaction> ApplyJsonTransactions(List<Transaction> transactions,List<TransactionTest> transactionTests,List<User> users)
        {
            List<Transaction> addedTransactions = new List<Transaction>();
            foreach(var item in transactionTests)
            {
                int rand = GenerateTestData.GetRandInt(0, users.Count());
                var user = users[rand];
                var datetime = item.OccurenceTime;
                var currency = item.Currency;
                var type = item.Type;
                var category = item.Category;
                var amount = item.Amount;
                var addedTransaction = new Transaction(user, type, category, currency, amount);
                addedTransactions.Add(addedTransaction);
            }
            transactions.AddRange(addedTransactions);
            return transactions;
        }
    }
}
