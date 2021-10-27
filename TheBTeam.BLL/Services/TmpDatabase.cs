using System.Collections.Generic;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL
{
    public class TmpDatabase //TODO: TmpDatabase
    {
        public static List<User> UsersList { get; set; } = new();
        public static List<Transaction> TransactionsList { get; set; } = new();
        public TmpDatabase()
        {
            UsersList = new List<User>();
            TransactionsList = new List<Transaction>();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }
    }
}
