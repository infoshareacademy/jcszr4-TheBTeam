using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Servises
{
    public class DataBase
    {
        public static List<User> AllUsers = new List<User>();
        public static string[] MainMenuItems = {
                "Load default setting",
                "Add new user",
                "Enter transation",
                "View users",
                "Show transaction history for the month",
                "Exit" };
        public void AddNewUser(User user)
        {
            AllUsers.Add(user);
        }
    }
}
