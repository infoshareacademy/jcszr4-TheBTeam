using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;


namespace TheBTeam.BLL.Services
{
    public class GenerateTestData
    {

        public static List<TransactionTest> PopulateTestTransactionList(List<User> users)
        {

            List<TransactionTest> testTransactionList = new List<TransactionTest>();
            Console.WriteLine("input the amount of transactions to generate");
            var amount = int.Parse(Console.ReadLine());
            while (amount >= 0)
            {
                var random = GetRandInt(0, users.Count());
                var TestObject = GenerateTestTransaction(users[random]);
                testTransactionList.Add(TestObject);
                amount--;

            }
            return testTransactionList;
        }
        public static TransactionTest GenerateTestTransaction(User user)
        {
            CategoryOfTransaction category = GetRandEnumCategory();
            string email = user.Email;
            TypeOfTransaction type = GetRandEnumType();
            Currency currency = Currency.PLN;
            decimal value = GetRandInt(100, 2000);
            TransactionTest TransactionTestObject = new TransactionTest(email, type, category, currency, value);
            
            return TransactionTestObject;
        }
        public static int GetRandInt(int min, int max)
        {
            Random rnd = new Random();
            int intValue = rnd.Next(min, max);
            return intValue;
        }
        public static CategoryOfTransaction GetRandEnumCategory()
        {
            Array values = Enum.GetValues(typeof(CategoryOfTransaction));
            Random random = new Random();
            CategoryOfTransaction category = (CategoryOfTransaction)values.GetValue(random.Next(values.Length));
            return category;
        }
        public static TypeOfTransaction GetRandEnumType()
        {
            Array values = Enum.GetValues(typeof(TypeOfTransaction));
            Random random = new Random();
            TypeOfTransaction category = (TypeOfTransaction)values.GetValue(random.Next(values.Length));
            return category;
        }
    }
}
