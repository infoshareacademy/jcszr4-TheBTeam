using System;
using System.Collections.Generic;
using TheBTeam.BLL.Models;
namespace TheBTeam.BLL.Servises
{
    public static class UserViewer
    {
        public static void ViewUsers(List<User> users)
        {
            if (users.Count > 0)
            {
                var textPaddingWidth = 20;
                var paddingChar = ' ';
                var numberOfCollumn = 8;
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                Console.WriteLine($"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Age".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Gender".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Company".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Balance".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Email".PadRight(textPaddingWidth, paddingChar)}");
                Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                if (users != null)
                {
                    foreach (var item in users)
                    {
                        Console.WriteLine($"|{item.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.Age.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.Gender.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Company.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Balance.ToString("C").PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Email.ToString().PadRight(textPaddingWidth, paddingChar)}");

                    }
                    Console.WriteLine(("").PadRight(textPaddingWidth * numberOfCollumn, '='));
                    Console.WriteLine($"Press any key to continue");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"No users, Please add new user");
                Console.WriteLine($"Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
