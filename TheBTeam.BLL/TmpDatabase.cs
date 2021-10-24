using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL
{
    public class  Library //TODO: TmpDatabase
    {
        public List<User> UsersList { get; }

        public Library()
        {
            UsersList = LoadDataFromFile.ReadUserFile();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }


    }
}
