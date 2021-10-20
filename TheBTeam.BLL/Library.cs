using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL
{
    class  Library
    {
        public List<User> UsersList { get; }

        public Library()
        {
            UsersList = LoReadUserFile();
        }
        public void AddNewUser(User user)
        {
            UsersList.Add(user);
        }


    }
}
