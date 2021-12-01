using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Validators;

namespace TheBTeam.BLL.Services
{
    public class EditExistingData
    {
        const int MinAddressLength = 3;
        const int MinNameLength = 2;
        private const int MinPhoneNumberLength = 12;
        const int MinAge = 13;
        const int MaxAge = 99;
        public static void EditUser(User user)
        {
            Console.WriteLine("EDIT USER\n=================");
            var tempuser = new User(user.FirstName, user.LastName, user.Gender, user.Age, user.Email, user.Phone, user.Address, user.Company, user.Currency, user.Balance);
            string options = "1)Edit surname\n2)Edit age\n3)Edit Company\n4)Edit gender\n5)Edit phone\n6)Edit balance\n7)Edit currency\n8)Finish\n9)Exit";
            bool exit = false;
            do
            {
                Console.WriteLine(options, "\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        tempuser.LastName = GetStringInput("Last Name", MinNameLength);
                        break;
                    case "2":
                        tempuser.Age = GetIntInput("Age", MinAge, MaxAge);
                        break;
                    case "3":
                        tempuser.Company = GetCompany();
                        break;
                    case "4":
                        tempuser.Gender = GetGender();
                        break;
                    case "5":
                        tempuser.Phone = GetPhoneNumber();
                        break;
                    case "6":
                        tempuser.Balance = GetDecimalInput("Current balance");
                        break;
                    case "7":
                        tempuser.Currency = GetCurrency();
                        break;
                    case "9":
                        return;
                    case "8":
                        {
                            Console.WriteLine("S to save, X to cancel\n");
                            while (true)
                            {
                                string key = Console.ReadKey().Key.ToString();
                                if (key.ToUpper() == "S")
                                {
                                    Console.WriteLine("\nSaved");
                                    if (user.LastName != tempuser.LastName)
                                    {
                                        Console.WriteLine($"{user.LastName} changed to {tempuser.LastName}");
                                    }
                                    if (user.Age != tempuser.Age)
                                    {
                                        Console.WriteLine($"{user.Age} changed to {tempuser.Age}");
                                    }
                                    if (user.Company != tempuser.Company)
                                    {
                                        Console.WriteLine($"{user.Company} changed to {tempuser.Company}");
                                    }
                                    if (user.Gender != tempuser.Gender)
                                    {
                                        Console.WriteLine($"{user.Gender} changed to {tempuser.Gender}");
                                    }
                                    if (user.Phone != tempuser.Phone)
                                    {
                                        Console.WriteLine($"{user.Phone} changed to {tempuser.Phone}");
                                    }
                                    if (user.Currency != tempuser.Currency)
                                    {
                                        Console.WriteLine($"{user.Currency} changed to {tempuser.Currency}");
                                    }
                                    if (user.Balance != tempuser.Balance)
                                    {
                                        Console.WriteLine($"{user.Balance} changed to {tempuser.Balance}");
                                    }
                                    user.LastName = tempuser.LastName;
                                    user.Age = tempuser.Age;
                                    user.Company = tempuser.Company;
                                    user.Gender = tempuser.Gender;
                                    user.Phone = tempuser.Phone;
                                    user.Currency = tempuser.Currency;
                                    user.Balance = tempuser.Balance;
                                    Console.ReadLine();
                                    exit = true;
                                    break;
                                }

                                else if (key.ToUpper() == "X")
                                {
                                    Console.WriteLine("\nCancelled");
                                    exit = true;
                                    break;

                                }
                                else
                                {
                                    Console.WriteLine("Select X to abort or S to save please\n");
                                }
                                
                            }
                            break;
                        }
                }
            } while (!exit);
        }

      /*  public static void EditTransaction(Transaction transaction)
        {
            Console.WriteLine("EDIT TRANSACTION\n==================");
            var tempTransaction = new Transaction(transaction.User, transaction.Type, transaction.Category, transaction.Currency, transaction.Amount);
            string options = "1)Edit category\n2)Save or cancel changes\n3)Exit";
            bool exit = false;
            do
            {
                Console.WriteLine(options, "\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        tempTransaction.Category = GetCategoryOfTransaction();
                        break;
                    case "2":
                        {
                            Console.WriteLine("S to save changes or X to cancel\n");
                            while (true)
                            {
                                string key = Console.ReadKey().Key.ToString();
                                if (key.ToUpper() == "S")
                                {
                                    if (transaction.Category != tempTransaction.Category)
                                    {
                                        Console.WriteLine($"{transaction.Category} changed to {tempTransaction.Category}");
                                    }
                                    transaction.Category = tempTransaction.Category;
                                    Console.WriteLine("\nSaved");
                                    Console.ReadLine();
                                    exit = true;
                                    break;
                                }
                                else if (key.ToUpper() == "X")
                                {
                                    Console.WriteLine("\nCanceled");
                                    exit = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Select S to save changes or X to cancel\n");
                                }
                            }

                            break;
                        }

                    case "3":
                        return;
                }
            } while (exit == false) ;
        }
*/
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
        private static string GetPhoneNumber()
        {
            while (true)
            {
                Console.Write("Phone Number(+XXXXXXXXXXX), or press enter if null: ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return null;

                var message = UserValidator.ValidatePhoneNumber(input, MinPhoneNumberLength);

                if (string.IsNullOrEmpty(message))
                    return input;

                if (message == "exit")
                    return null;

                Console.WriteLine(message);
            }
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
    }
}