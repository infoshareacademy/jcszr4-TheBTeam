using System;
using System.Collections.Generic;
using System.Text;

namespace TheBTeam.BLL.Servises
{
    public class UserViewer
    {
        public string ViewUsers(List<User> users)
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
            report.AppendLine(new String('-', 97));
            //TmpDatabase tmpUserList = new TmpDatabase();
            if (users != null)
            {
                foreach (var item in users)
                {
                    report.AppendLine($"|{item.FirstName,lenghtFirstName}|" +
                                      $"{item.LastName,lenghtLastName}|" +
                                      $"{item.Age,lenghtAge}|" +
                                      $"{item.Gender,lenghtGender}|" +
                                      $"{item.Company,lenghtCompany}|" +
                                      $"{item.Email,lenghtEmail}|");
                }
            }
            return report.ToString();
        }
    }
}
