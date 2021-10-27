using System;
using System.Collections.Generic;
using System.Text;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL.Servises
{
    public static class UserViewer
    {
        public static void ViewUsers(List<User> users)
        {
            var textPaddingWidth = 25;
            var paddingChar = ' ';
            Console.WriteLine($"|{"FirstName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"LastName".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Age".PadRight(textPaddingWidth, paddingChar)} " +
                                  $"|{"Gender".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Company".PadRight(textPaddingWidth, paddingChar)}" +
                                  $"|{"Email".PadRight(textPaddingWidth, paddingChar)}");
            Console.WriteLine();
            if (users != null)
            {
                foreach (var item in users)
                {
                        Console.WriteLine($"|{item.FirstName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.LastName.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.Age.ToString().PadRight(textPaddingWidth, paddingChar)} " +
                                          $"|{item.Gender.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Company.ToString().PadRight(textPaddingWidth, paddingChar)}" +
                                          $"|{item.Email.ToString().PadRight(textPaddingWidth, paddingChar)}");
                    }
            }
        }
    }
}
