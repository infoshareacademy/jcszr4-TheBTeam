using System;
using System.Runtime;
using System.Collections.Generic;
using System.Threading.Channels;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using Type = System.Type;

namespace TheBTeam.ConsoleApp
{
    public class ConsoleFactory
    {
        const int MinAddressLength = 3;
        const int MinNameLength = 2;
        const int MinPhoneNumberLength = 9;
        const int MinAge = 18;
        const int MaxAge = 99;
        public static Transaction CreateNewTransaction(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("                       CREATING NEW TRANSACTION                  ");
            Console.WriteLine("=================================================================");
            User outuser = GetOuterUser(users);
            decimal Amount = GetDecimalInput("Amount"); ;
            var currency = GetCurrency();
            var categoryOfTransaction = GetCategoryOfTransaction();
            var typeOfTransaction = GetTypeOfTransaction();
            var titleOfTransaction = GetStringInput("title", 5);
            Console.WriteLine("=================================================================");
            return new Transaction(outuser, DateTime.Now, currency, typeOfTransaction, categoryOfTransaction, Amount, titleOfTransaction);
        }
        private static User GetOuterUser(List<User> users)
        {
            User outuser = null;
            Console.Write($"Enter LastName: ");
            string lastName = Console.ReadLine();
            foreach (var item in users)
            {
                if (item.LastName == lastName)
                {
                    outuser = item;
                }
            }
            if (outuser == null)
            {
                Console.Write($"{lastName} doesn't exsist!\n");
            }
            //if (outuser == null) throw new NullReferenceException();
            return outuser;
        }

        public static User CreateNewUser()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("                       CREATING NEW USER                        ");
                Console.WriteLine("=================================================================");
                Console.WriteLine("Type follwoing informations, or type 'Exit' to abort creating new user");

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
        public static string GetStringInput(string name, int minLength)
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

                Console.WriteLine($"{name} should be more than 0");
            }
        }

        public static TypeOfTransaction GetTypeOfTransaction()
        {
            var currentArray = Enum.GetNames(typeof(TypeOfTransaction));
            Console.WriteLine("Choose your type of transaction:");
            for (int i = 0; i < currentArray.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {currentArray[i]}");
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

                if (isParsed && selection < currentArray.Length)
                    return (TypeOfTransaction)selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
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

        public static User EditUser(User user)
        {
            User tempuser = user;
            string options = "1)to edit name\n2)to edit surname\n3)to edit email\n4to edit age\n5)to edit phone\n6) to edit balance\n7)to edit currency\n8)to save or commit changes";
            //Console.WriteLine(options);
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine(options,"\n");
                string choice = Console.ReadLine();
                if (choice == "1")//name
                {
                    tempuser.FirstName = GetStringInput("First Name", MinNameLength);
                }
                else if (choice == "2")//surname
                {
                    tempuser.LastName = GetStringInput("Last Name", MinNameLength);
                }
                else if (choice == "3")//email
                {
                    tempuser.Email = GetEmail();
                }
                else if (choice == "4")//age
                {
                    tempuser.Age = GetIntInput("Age", MinAge, MaxAge);
                }
                else if (choice == "5")//phone
                {
                    tempuser.Phone = GetPhoneNumber();
                }
                else if (choice == "6")//balance
                {
                    tempuser.Balance = GetDecimalInput("current balance");
                }
                else if (choice == "7")//curency
                {
                    tempuser.Currency = GetCurrency();
                }
                else if (choice == "8")//save or agort
                {
                    Console.WriteLine("Press S to save changes or X to abort");
                    bool choiceSubmitted = false;
                    while (choiceSubmitted == false)
                    {
                        string key = Console.ReadKey().Key.ToString();
                        if (key.ToUpper() == "S")
                        {
                            user = tempuser;
                            choiceSubmitted = true;
                            exit = true;
                        }
                         
                        else if (key.ToUpper() == "X")
                        {
                            choiceSubmitted = true;
                            exit = true;
                            
                        }
                        else
                        {
                            Console.WriteLine("only X to abort or S to save please");
                        }
                    }
                }
            }
            return user;
        }
        public static Transaction EditTransaction(Transaction transaction)
        {
            Transaction tempTransaction = transaction;
            string options = "1)to edit occurence date\n2)to edit currency\n3)to edit type of transaction\n4to edit amount\n5)to edit category\n)6)to commit or abort changes";
            //Console.WriteLine(options);
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine(options, "\n");
                string choice = Console.ReadLine();
                if (choice == "1")//occurence date
                {
                    Console.WriteLine("not yet implemented");
                   // tempTransaction.OccurenceTime = GetStringInput("First Name", MinNameLength);
                }
                else if (choice == "2")//currency
                {
                    tempTransaction.Currency = GetCurrency();
                }
                else if (choice == "3")//type
                {
                    tempTransaction.Type = GetTypeOfTransaction();
                }
                else if (choice == "4")//amount
                {
                    tempTransaction.Amount = GetDecimalInput("new amount");
                }
                else if (choice == "5")//category
                {
                    tempTransaction.Category = GetCategoryOfTransaction();
                }
                else if (choice == "6")//save or agort
                {
                    Console.WriteLine("Press S to save changes or X to abort");
                    bool choiceSubmitted = false;
                    while (choiceSubmitted == false)
                    {
                        string key = Console.ReadKey().Key.ToString();
                        if (key.ToUpper() == "S")
                        {
                            transaction = tempTransaction;
                            choiceSubmitted = true;
                            exit = true;
                        }

                        else if (key.ToUpper() == "X")
                        {
                            choiceSubmitted = true;
                            exit = true;

                        }
                        else
                        {
                            Console.WriteLine("only X to abort or S to save please");
                        }
                    }
                }
            }
            return transaction;
        }
    }
}


