using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL.Services
{
    public class ExportTransactionToJSON
    {
        public static void exportTransactions(List<TransactionTest> transactions)
        {
            //var directoryName = "SourceFiles";
            //var directoryExists = Directory.Exists(directoryName);
            //if (directoryExists == false)
            //{
            //    Directory.CreateDirectory(directoryName);
            //}
            var fileName = "Transactions.json";
            //var filePath = directoryName + @"\" + fileName;
            //var exists = File.Exists(filePath);
            string jsonString = "";
            jsonString = JsonConvert.SerializeObject(transactions);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
