using System.Collections.Generic;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.BLL
{
    public class TmpDatabase //TODO: TmpDatabase
    {
        public List<User> UsersList { get; set; }
        public List<Transaction> TransactionsList { get; set; } = new();
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
