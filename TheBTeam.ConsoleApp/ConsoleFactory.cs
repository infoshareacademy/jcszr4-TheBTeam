using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Validators;


namespace TheBTeam.ConsoleApp
{
    public class ConsoleFactory
    {
        const int MinAddressLength = 3;
        const int MinNameLength = 2;
        private const int MinPhoneNumberLength = 12;
        const int MinAge = 13;
        const int MaxAge = 99;


        public static User CreateNewUser()
        {

            while (true)
            {

                Header("CREATING NEW USER");

                var firstName = GetStringInput("First Name", MinNameLength);
                if (firstName.ToLower() == "exit")
                    return null;
                Header("CREATING NEW USER");
                var lastName = GetStringInput("Last Name", MinNameLength);
                if (lastName.ToLower() == "exit")
                    return null;
                Header("CREATING NEW USER");
                var gender = GetGender();
                Header("CREATING NEW USER");
                var age = GetIntInput("Age", MinAge, MaxAge);
                if (age == 0)
                    return null;
                Header("CREATING NEW USER");
                var email = GetEmail();
                if (email == null)
                    return null;
                Header("CREATING NEW USER");
                var phone = GetPhoneNumber();
                if (phone == null)
                    return null;
                Header("CREATING NEW USER");
                var address = GetAddress();
                Header("CREATING NEW USER");
                var company = GetCompany();
                if (company.ToLower() == "exit")
                    return null;
                Header("CREATING NEW USER");
                var currency = GetCurrency();
                Header("CREATING NEW USER");
                var balance = GetDecimalInput("current balance");
                if (balance == -69)
                    return null;

                Console.WriteLine("=================================================================");
                Console.WriteLine("User created successfully! Press any key to continue.");
                Console.ReadKey();

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
            ApplyTransaction(transaction, user);
            Console.WriteLine("=================================================================");
            Console.WriteLine($"Transaction applied successfully! Press any key to continue.");
            Console.ReadKey();

            return transaction;

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
                var input = string.Empty;
                if (minLength == 0)
                {
                    Console.Write($"{name}(press enter if null): ");
                    return Console.ReadLine()?.Trim();
                }

                Console.Write($"{name}: ");
                input = Console.ReadLine()?.Trim();
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
                Console.Write($"Company(press enter if null): ");
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
                var message = UserValidator.ValidateEmail(input);
                
                if (string.IsNullOrEmpty(message))
                {
                    return input;
                }
                Console.WriteLine(message);
            }
        }
        private static string GetPhoneNumber()
        {
            while (true)
            {
                Console.Write("Phone Number(+XXXXXXXXXXX), or press enter if null: ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return null;

                if (input.Length < MinPhoneNumberLength)
                {
                    Console.WriteLine(
                        $"Invalid phone number! Phone number have to have at least {MinPhoneNumberLength} digits, or type 'Exit' to abort. Retry!");
                    continue;
                }
                if (input.ToLower() == "exit")
                    return null;
                if (!input.StartsWith('+'))
                    Console.WriteLine("Invalid phone number! Phone number have to start with country code eg. +48. Retry!");

                if (!int.TryParse(input, out var intInput))
                {
                    Console.WriteLine("Invalid input, it's not a number");
                    continue;
                }

                return input;
            }
        }
        private static string GetAddress()
        {
            var city = GetStringInput("City", 0);
            if (string.IsNullOrEmpty(city))
                return null;
            var addressList = new List<string>()
            {
            city,
            GetStringInput("Street", 3),
            GetStringInput("Province", 3),
            GetStringInput("Postal code", 5)
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
                var input = Console.ReadLine().Replace(',', '.');
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
        public static string SelectUserEmail(List<User> usersList)
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

                return user.Email;
            } while (true);

        }
        public static TypeOfTransaction GetTypeOfTransaction()
        {

            var typeArray = Enum.GetNames(typeof(TypeOfTransaction));

            Console.WriteLine("Choose type of transaction:");
            for (int i = 0; i < typeArray.Length; i++)
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

                if (isParsed && selection <= typeArray.Length)
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
                var input = Console.ReadLine();
                Console.WriteLine();
                //var isParesd = int.TryParse(input, out var selection);
                if (!int.TryParse(input, out var selection))
                {
                    Console.WriteLine("Wrong value, try again!\n");
                    continue;
                }

                if (selection <= categoryArray.Length)
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
            
            Console.WriteLine($"Current balance: {user.Balance.ToString("C", CultureInfo.CurrentCulture)}");
            return transaction;
        }
        public static void EditUser(User user)
        {
            var tempuser = new User(user.FirstName, user.LastName, user.Gender, user.Age, user.Email, user.Phone, user.Address, user.Company, user.Currency, user.Balance);
            string options = "1)Edit surname\n2)Edit age\n3)Edit Company\n4)Edit gender\n5)Edit phone\n6)Edit balance\n7)Edit currency\n8)Finish";
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine(options, "\n");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    tempuser.LastName = GetStringInput("Last Name", MinNameLength);
                }
                else if (choice == "2")
                {
                    tempuser.Age = GetIntInput("Age", MinAge, MaxAge);
                }
                else if (choice == "3")
                {
                    tempuser.Company = GetCompany();
                }
                else if (choice == "4")
                {
                    tempuser.Gender = GetGender();
                }
                else if (choice == "5")
                {
                    tempuser.Phone = GetPhoneNumber();
                }
                else if (choice == "6")
                {
                    tempuser.Balance = GetDecimalInput("Current balance");
                }
                else if (choice == "7")
                {
                    tempuser.Currency = GetCurrency();
                }
                else if (choice == "8")
                {
                    Console.WriteLine(" S to save, X to cancel\n");
                    bool choiceSubmitted = false;
                    while (choiceSubmitted == false)
                    {
                        string key = Console.ReadKey().Key.ToString();
                        if (key.ToUpper() == "S")
                        {
                            user.LastName = tempuser.LastName;
                            user.Age = tempuser.Age;
                            user.Company = tempuser.Company;
                            user.Gender = tempuser.Gender;
                            user.Phone = tempuser.Phone;
                            user.Currency = tempuser.Currency;
                            user.Balance = tempuser.Balance;
                            Console.WriteLine("\nSaved");
                            choiceSubmitted = true;
                            exit = true;
                        }

                        else if (key.ToUpper() == "X")
                        {
                            Console.WriteLine("\nCancelled");
                            choiceSubmitted = true;
                            exit = true;

                        }
                        else
                        {
                            Console.WriteLine("Select X to abort or S to save please\n");
                        }
                    }
                }
            }
        }
        public static void EditTransaction(Transaction transaction)
        {
            var tempTransaction = new Transaction(transaction.User, transaction.Type, transaction.Category, transaction.Currency, transaction.Amount);
            string options = "1)Edit category\n2)Save or cancel changes";
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine(options, "\n");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    tempTransaction.Category = GetCategoryOfTransaction();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("S to save changes or X to cancel\n");
                    bool choiceSubmitted = false;
                    while (choiceSubmitted == false)
                    {
                        string key = Console.ReadKey().Key.ToString();
                        if (key.ToUpper() == "S")
                        {
                            transaction.Category = tempTransaction.Category;
                            choiceSubmitted = true;
                            Console.WriteLine("\n Saved");
                            exit = true;
                        }
                        else if (key.ToUpper() == "X")
                        {
                            choiceSubmitted = true;
                            Console.WriteLine("\nCanceled");
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Select S to save changes or X to cancel\n");
                        }
                    }
                }
            }
        }
    }
}

