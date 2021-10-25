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
        public List<User> UsersList { get; set; }
        public List<Transaction> TransactionsList { get; }
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
