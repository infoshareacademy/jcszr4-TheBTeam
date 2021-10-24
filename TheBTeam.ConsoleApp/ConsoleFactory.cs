using System;
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
        const int MinCompanyLength = 3;

        public static User CreateNewUser()
        {

            Console.Clear();
            Console.WriteLine("                       CREATING NEW USER                        ");
            Console.WriteLine("=================================================================");

            var firstName = GetStringInput("First Name", MinNameLength);
            var lastName = GetStringInput("Last Name", MinNameLength);
            var gender = GetGender();
            var age = GetIntInput("Age", MinAge, MaxAge);
            var email = GetEmail();
            var phone = GetPhoneNumber();
            var address = GetAddress();
            var company = GetCompany();
            var currency = GetCurrency();
            var balance = GetDecimalInput("current balance");

            Console.WriteLine("=================================================================");

            return new User(firstName, lastName, gender, age, email, phone, address, company, currency, balance);
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
                {
                    return input;
                }
            }
        }
        private static string GetCompany()
        {
            while (true)
            {
                Console.Write($"Company: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null)
                    return null;
                else
                {
                    return input;
                }
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

                if (isParsed && selection < genderArray.Length)
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
                var isDig = int.TryParse(input, out var result);
                if (isDig && result >= min && result <= max)
                    return result;

                Console.WriteLine($"{name} should be between {min} and {max}");
            }
        }
        private static string GetEmail()
        {
            while (true)
            {
                Console.Write("email: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null)
                    Console.WriteLine("input is empty, retry!");
                else if (!input.Contains('@') | !input.Contains('.'))
                    Console.WriteLine("Email have to contain @ and .***, retry!");
                else
                    return input;
            }
        }
        private static string GetPhoneNumber()
        {
            while (true)
            {
                Console.Write("Phone Number: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null || input.Length < MinPhoneNumberLength)
                    Console.WriteLine(
                        $"Invalid phone number! Phone number have to have at least {MinPhoneNumberLength} digits . Retry!");
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

                if (isParsed && selection < currenciesArray.Length)
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
                var isDig = decimal.TryParse(input, out var result);
                if (isDig && result >= 0)
                    return result;

                Console.WriteLine($"{name} should be more than 0");
            }
        }
    }
}


