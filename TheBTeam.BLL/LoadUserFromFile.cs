using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheBTeam.BLL
{
    public class LoadUserFromFile
    {
        public static List<User> ReadUserFile()
        {
            string fileName = @"SourceFiles\users.json";
            string jsonstring = File.ReadAllText(fileName);
            List<User> userData = JsonConvert.DeserializeObject<List<User>>(jsonstring);          
            return userData;
        }
    }
}
