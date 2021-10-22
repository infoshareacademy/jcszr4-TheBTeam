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
     }
}
