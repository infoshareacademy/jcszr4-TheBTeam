using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TheBTeam.BLL.Model;

namespace TheBTeam.BLL.Services
{
    public class LoadDataFromFile
    {
        public static List<User> ReadUserFile()
        {
            string fileName = @"SourceFiles\users.json";
            string jsonString = File.ReadAllText(fileName);
            List<User> userData = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return userData;
        }
    }
}
