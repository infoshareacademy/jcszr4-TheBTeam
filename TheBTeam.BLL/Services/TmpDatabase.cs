using System.Collections.Generic;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;

namespace TheBTeam.BLL.Services
{
    public class TmpDatabase //TODO: TmpDatabase
    {
        public List<UserDto> UsersList { get; set; }
        public List<TransactionDto> TransactionsList { get; set; } = new();
        public TmpDatabase()
        {
            UsersList = new List<UserDto>();
            TransactionsList = new List<TransactionDto>();
            //UsersList = LoadDataFromFile.ReadUserFile();
        }
        public void AddNewUser(UserDto userDto)
        {
            UsersList.Add(userDto);
        }


    }
}
