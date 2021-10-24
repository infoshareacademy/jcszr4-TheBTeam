using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.BLL
{
    public class  TmpDatabase //TODO: TmpDatabase
    {
        public List<User> UsersList { get; }
        public List<Transaction> TransactionsList { get; }

        public TmpDatabase()
        {
            UsersList = LoadDataFromFile.ReadUserFile();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }


    }
}
