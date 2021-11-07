using System.Collections.Generic;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.BLL.Services
{
    public class TmpDatabase //TODO: TmpDatabase
    {
        public List<User> UsersList { get; set; }
        public List<Transaction> TransactionsList { get; set; } = new();
        public TmpDatabase()
        {
            UsersList = new List<User>();
            TransactionsList = new List<Transaction>();
            //UsersList = LoadDataFromFile.ReadUserFile();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }


    }
}
