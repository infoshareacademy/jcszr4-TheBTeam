using System.Collections.Generic;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL
{
    public class TmpDatabase //TODO: TmpDatabase
    {
        public List<User> UsersList { get; set; }
        public List<Transaction> TransactionsList { get; }
        public TmpDatabase()
        {
            //UsersList = LoadDataFromFile.ReadUserFile();
            UsersList = new List<User>();
            TransactionsList = new List<Transaction>();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }
    }
}
