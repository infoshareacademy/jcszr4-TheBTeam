using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;


namespace TheBTeam.ConsoleApp
{
    public class ConsoleFactory
    {
        const int MinAddressLength = 3;
        const int MinNameLength = 2;
        const int MinPhoneNumberLength = 9;
        const int MinAge = 18;
        const int MaxAge = 99;


        public static User CreateNewUser()
        {

            while (true)
            {

                Header("CREATING NEW USER");

                var firstName = GetStringInput("First Name", MinNameLength);
                if (firstName.ToLower() == "exit")
                    return null;
                var lastName = GetStringInput("Last Name", MinNameLength);
                if (lastName.ToLower() == "exit")
                    return null;
                var gender = GetGender();
                var age = GetIntInput("Age", MinAge, MaxAge);
                if (age == 0)
                    return null;
                var email = GetEmail();
                if (email == null)
                    return null;
                var phone = GetPhoneNumber();
                if (phone == null)
                    return null;
                var address = GetAddress();
                var company = GetCompany();
                if (company.ToLower() == "exit")
                    return null;
                var currency = GetCurrency();
                var balance = GetDecimalInput("current balance");
                if (balance == -69)
                    return null;

                Console.WriteLine("=================================================================");

                return new User(firstName, lastName, gender, age, email, phone, address, company, currency, balance);
            }
        }

        public static Transaction AddNewTransaction(List<User> usersList)
        {
            Header("ADDING NEW TRANSACTION");
            var user = SelectUser(usersList);
            if (user == null)
                return null;
            Header("ADDING NEW TRANSACTION");
            var type = GetTypeOfTransaction();
            Header("ADDING NEW TRANSACTION");
            var category = GetCategoryOfTransaction();
            Header("ADDING NEW TRANSACTION");
            var currency = GetCurrency();
            Header("ADDING NEW TRANSACTION");
            Console.WriteLine($"Current balance: {user.Balance.ToString("C", CultureInfo.CurrentCulture)}");
            var amount = GetDecimalInput("Amount");
            if (amount == -69)
                return null;

            var transaction = new Transaction(user, type, category, currency, amount);
            return ApplyTransaction(transaction, user);

        }
        public static void Header(string name)
        {
            Console.Clear();
            Console.WriteLine($"                       {name}                        ");
            Console.WriteLine("=================================================================");
            Console.WriteLine("Type following information, or type 'Exit' to abort creating new user");
        }
        private static string GetStringInput(string name, int minLength)
        {
            while (true)
            {
                Console.Write($"{name}: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null || input.Length < minLength)
                    Console.WriteLine($"Invalid data. {name} should have at least {minLength} char long. Retry!");
                else
                    return input;

            }
        }
        private static string GetCompany()
        {
            while (true)
            {
                Console.Write($"Company: ");
                var input = Console.ReadLine()?.Trim();

                return input;
            }
        }
        private static Gender GetGender()
        {
            var genderArray = Enum.GetNames(typeof(Gender));

            Console.WriteLine("Choose your gender:");
            for (int i = 0; i < genderArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {genderArray[i]}");
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

                if (isParsed && selection <= genderArray.Length)
                    return (Gender)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        private static int GetIntInput(string name, int min, int max)
        {
            while (true)
            {
                Console.Write($"{name}: ");
                var input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    return 0;
                var isDig = int.TryParse(input, out var result);
                if (isDig && result >= min && result <= max)
                    return result;

                Console.WriteLine($"{name} should be between {min} and {max}");
            }
        }
        public static string GetEmail()
        {
            while (true)
            {
                Console.Write("email: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null)
                    Console.WriteLine("Input is empty, retry!");
                else if (input.ToLower() == "exit")
                    return null;
                else if (!input.Contains('@') | !input.Contains('.') || input.Length < 7)
                    Console.WriteLine("Email have to contain @ and .***, retry!");
                else if (input.LastIndexOf(".", StringComparison.Ordinal) > input.Length - 3)
                    Console.WriteLine("Email should have at least 2 chars after .");
                else
                    return input;
            }
        }
        private static string GetPhoneNumber()
        {
            while (true)
            {
                Console.Write("Phone Number(+XX XXX XXX XXX): ");
                var input = Console.ReadLine()?.Trim();
                if (input == null || input.Length < MinPhoneNumberLength)
                    Console.WriteLine(
                        $"Invalid phone number! Phone number have to have at least {MinPhoneNumberLength} digits, or type 'Exit' to abort. Retry!");
                else if (input.ToLower() == "exit")
                    return null;
                else if (!input.StartsWith('+'))
                    Console.WriteLine("Invalid phone number! Phone number have to start with country code eg. +48. Retry!");
                else
                    return input;
            }
        }
        private static string GetAddress()
        {
            var addressList = new List<string>()
            {
            GetStringInput("Street", MinAddressLength),
            GetStringInput("City", MinAddressLength),
            GetStringInput("Province", MinAddressLength),
            GetStringInput("Postal code", MinAddressLength)
            };

            var address = String.Join(", ", addressList);
            return address;
        }
        private static Currency GetCurrency()
        {
            var currenciesArray = Enum.GetNames(typeof(Currency));

            Console.WriteLine("Choose your currency:");
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

                if (isParsed && selection <= currenciesArray.Length)
                    return (Currency)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        private static decimal GetDecimalInput(string name)
        {
            while (true)
            {
                Console.Write($"{name}: ");
                var input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    return -69;
                var isDig = decimal.TryParse(input, out var result);
                if (isDig && result >= 0)
                    return result;

                Console.WriteLine($"{name} should be no less than 0");
            }
        }
        public static User SelectUser(List<User> usersList)
        {
            do
            {
                var email = GetEmail();
                if (email == null)
                {
                    return null;
                }
                var user = usersList.FirstOrDefault(user => user.Email == email);
                if (user == null)
                {
                    Console.WriteLine($"There is no user with {email} email, retry or type Exit to cancel");
                    continue;
                }

                return user;
            } while (true);

        }
        public static TypeOfTransaction GetTypeOfTransaction()
        {

            var typeArray = Enum.GetNames(typeof(TypeOfTransaction));

            Console.WriteLine("Choose type of transaction:");
            for (int i = 0; i < typeArray.Length - 1; i++)
            {
                Console.WriteLine($"{i + 1}. {typeArray[i]}");
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

                if (isParsed && selection < typeArray.Length)
                    return (TypeOfTransaction)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        public static CategoryOfTransaction GetCategoryOfTransaction()
        {

            var categoryArray = Enum.GetNames(typeof(CategoryOfTransaction));

            Console.WriteLine("Choose category of transaction:");
            for (int i = 0; i < categoryArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categoryArray[i]}");
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

                if (isParsed && selection <= categoryArray.Length)
                    return (CategoryOfTransaction)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        public static Transaction ApplyTransaction(Transaction transaction, User user)
        {

            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += transaction.Amount;
                Console.WriteLine("Income added");
            }
            else if (user.Balance >= transaction.Amount)
            {
                user.Balance -= transaction.Amount;
                Console.WriteLine("Outcome added");
            }
            else
            {
                transaction.Type = TypeOfTransaction.Canceled;
                Console.WriteLine("Transaction rejected, your balance is to low!");
            }
            Console.WriteLine($"Current balance: {user.Balance.ToString("C", CultureInfo.CurrentCulture)}");
            return transaction;
        }
    }
}


