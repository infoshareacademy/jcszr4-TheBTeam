using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.Services

{
    public class LoadDataFromFile
    {

        private readonly UserService _userService;

        public static List<UserDto> ReadUserFile()
        {
            string fileName = @"SourceFiles\users.json";

            string jsonString = File.ReadAllText(fileName);
            List<UserDto> userData = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
            return userData;
        }
        public static List<User> ReadDalUserFile()
        {
            string fileName = @"SourceFiles\users.json";

            string jsonString = File.ReadAllText(fileName);
            List<User> userData = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return userData;
        }

        public static void LoadUsersToDatbase(PlannerContext plannerContext)
        {
            if (!plannerContext.Users.Any())
            {
                var users = LoadDataFromFile.ReadDalUserFile();
                plannerContext.AddRange(users);
                plannerContext.SaveChanges();
            }
        }
    }
    
}
