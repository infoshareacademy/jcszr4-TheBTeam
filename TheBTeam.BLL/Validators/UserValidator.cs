using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Validators
{
    public static class UserValidator
    {
        public static string ValidateEmail(string input)
        {
            if (input == null)
                return "Input is empty, retry!";
            else if (input.ToLower() == "exit")
                return "exit";
            else if (!input.Contains('@') | !input.Contains('.') || input.Length < 7)
                return "Email have to contain @ and .***, retry!";
            else if (input.LastIndexOf(".", StringComparison.Ordinal) > input.Length - 3)
                return "Email should have at least 2 chars after .";
            return string.Empty;
        }

        public static string ValidatePhoneNumber(string input, int minPhoneNumberLength)
        {
            if (input.Length < minPhoneNumberLength)
            {
                return $"Invalid phone number! Phone number have to have at least {minPhoneNumberLength} digits, or type 'Exit' to abort. Retry!");
            }
            if (input.ToLower() == "exit")
                return "exit";
            if (!input.StartsWith('+'))
                return  "Invalid phone number! Phone number have to start with country code eg. +48. Retry!");

            if (!int.TryParse(input, out var intInput))
            {
                Console.WriteLine("Invalid input, it's not a number");
                continue;
            }
        }

    }
}
