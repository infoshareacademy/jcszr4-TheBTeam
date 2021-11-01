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
                return null;
            else if (!input.Contains('@') | !input.Contains('.') || input.Length < 7)
                return "Email have to contain @ and .***, retry or select exit to quit to main menu";
            else if (input.LastIndexOf(".", StringComparison.Ordinal) > input.Length - 3)
                return "Email should have at least 2 chars after or select exit to quit to main menu.";
            return string.Empty;
        }

    }
}
