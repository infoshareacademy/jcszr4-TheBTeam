using Microsoft.AspNetCore.Identity;
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

        public static void SeedDatabase(PlannerContext plannerContext, IPasswordHasher<User> hasher)
        {
            if (!plannerContext.Users.Any())
            {
                var adminRole = new Role()
                {
                    Name = "Admin"
                };

                var userRole = new Role()
                {
                    Name = "User"
                };

                plannerContext.Roles.Add(adminRole);
                plannerContext.Roles.Add(userRole);

                var admin = new User
                {
                    Address = "",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Age = 99,
                    Gender = Gender.Male,
                    Company = "",
                    Email = "admin@admin.eu",
                    Role = adminRole
                };

                var password = hasher.HashPassword(admin, "bTeamRoxs");
                admin.PasswordHash = password;
                plannerContext.Add(admin);
                plannerContext.SaveChanges();
            }
        }
    }
}
