using System;
using System.Collections.Generic;
using TheBTeam.BLL;
using TheBTeam.BLL.Model;
using Type = System.Type;

namespace TheBTeam.ConsoleApp
{

    public class ConsoleFactory
    {
        public static User CreateNewUser(List<User> userList)
        {
            const int minNameLength = 2;
            const int minPhoneNumberLength = 9;
            const int minAge = 18;
            const int maxAge = 99;
            const int minAddressLength = 3;
            const int minCompanyLength = 3;

            Console.Clear();
            Console.WriteLine("                       CREATING NEW USER                        ");
            Console.WriteLine("=================================================================");

            var firstName = GetStringInput("First Name", minNameLength);
            var lastName = GetStringInput("Last Name", minNameLength);
            var gender = GetGender();
            var age = GetIntInput("Age", minAge, maxAge);
            var email = GetEmail();
            var phone = GetPhoneNumber(minPhoneNumberLength);
            var address = GetAddress(minAddressLength);
            var company = GetStringInput("Company", minCompanyLength);
            var currency = GetCurrency();

            Console.WriteLine("=================================================================");
            return new User(firstName, lastName, gender, age, email, phone, address, company,currency,age);
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
        private static string GetPhoneNumber(int minLength)
        {
            while (true)
            {
                Console.Write("Phone Number: ");
                var input = Console.ReadLine()?.Trim();
                if (input == null || input.Length < minLength)
                    Console.WriteLine(
                        $"Invalid phone number! Phone number have to have at least {minLength} digits . Retry!");
                else if (!input.StartsWith('+'))
                    Console.WriteLine("Invalid phone number! Phone number have to start with country code eg. +48. Retry!");
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
                var isDig = int.TryParse(input, out var result);
                if (isDig && result >= min && result <= max)
                    return result;

                Console.WriteLine($"{name} should be between {min} and {max}");
            }
        }
        public static Gender GetGender()
        {
            var genders = new TypeLists().Genders;
            Console.WriteLine("Choose your gender:");
            for (int i = 0; i < genders.Count; i++)
                Console.WriteLine($"{i + 1}. {genders[i]}");

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

                if (isParsed && selection <= genders.Count)
                    return genders[selection - 1];

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        private static string GetAddress(int min)
        {
            var addressList = new List<string>()
            {
            GetStringInput("Street", min),
            GetStringInput("City", min),
            GetStringInput("Province", min),
            GetStringInput("Postal code", min)
            };

            var address = String.Join(", ", addressList);
            return address;
        }
        private static Currency GetCurrency()
        {
            var currencies = new TypeLists().Currencies;
            Console.WriteLine("Choose your currency:");
            for (int i = 0; i < currencies.Count; i++)
                Console.WriteLine($"{i + 1}. {currencies[i]}");

            while (true)
            {
                var input = Console.ReadLine();

                var isParsed = int.TryParse(input, out int selection);

                if (isParsed && selection <= currencies.Count)
                    return currencies[selection - 1];

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
        public static int SelectFromList(string name, List<Category> list)//TO DO: Check if possible
        {
            Console.WriteLine($"Choose your {name}:");
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}. {list[i].ToString()}");

            while (true)
            {
                var input = Console.ReadLine();

                var isParsed = int.TryParse(input, out int selection);

                if (isParsed && selection <= list.Count)
                    return selection - 1;

                Console.WriteLine("Wrong selection, try Again!");
            }
        }
    }
}


