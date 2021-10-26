using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.BLL
{
    public class  DataBase
    {
        public List<User> UsersList { get; }

        public DataBase()
        {
            UsersList = LoadDataFromFile.ReadUserFile();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }


    }
}
