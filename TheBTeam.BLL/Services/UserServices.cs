using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Servises
{
    public class UserServices
    {
        public string ViewUsers()
        {
            const int lenghtFirstName = -15;
            const int lenghtLastName = -15;
            const int lenghtAge = -5;
            const int lenghtGender = -10;
            const int lenghtCompany = -15;
            const int lenghtEmail = -30;
            var report = new StringBuilder();
            report.AppendLine($"|{"FirstName",lenghtFirstName}|{"LastName",lenghtLastName}|" +
                               $"{"Age",lenghtAge}|{"Gender",lenghtGender}|" +
                               $"{"Company",lenghtCompany}|{"Email",lenghtEmail}|");
            foreach (var item in DataBase.AllUsers)
            {
                report.AppendLine($"|{item.FirstName,lenghtFirstName}|" +
                                  $"{item.LastName,lenghtLastName}|" +
                                  $"{item.Age,lenghtAge}|" +
                                  $"{item.Gender,lenghtGender}|" +
                                  $"{item.Company,lenghtCompany}|" +
                                  $"{item.Email,lenghtEmail}|");
            }
            return report.ToString();
        }
        public void CreateNewUser()
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
            var gender = "Male";
            var age = GetIntInput("Age", minAge, maxAge);
            var email = GetEmail();
            var phone = GetPhoneNumber(minPhoneNumberLength);
            var address = GetAddress(minAddressLength);
            var company = GetStringInput("Company", minCompanyLength);
            var currency = "zl";

            Console.WriteLine("=================================================================");
            DataBase.AllUsers.Add(new User("Idy", true, 122, currency, age, firstName, lastName, gender, company, email, phone, address, DateTime.Now));
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


    }
}
