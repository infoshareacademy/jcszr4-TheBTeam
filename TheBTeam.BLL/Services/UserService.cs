using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;


namespace TheBTeam.BLL.Services
{
    public class UserService
    {
        private static List<User> Users = LoadDataFromFile.ReadUserFile();
        public List<User> GetAll()
        {
            return Users;
        }
        public User GetById(string id)
        {
            return Users.SingleOrDefault(m => m.Id == id);
        }
        public void Create(User model)
        {
            model.Id = GetNextId();

            Users.Add(model);
        }
        public string GetNextId()
        {
            if (!Users.Any())
                return String.Empty;
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public void Delete(string id)
        {
            var user = GetById(id);
            Users.Remove(user);
        }

        public void Update(User model)
        {
            var user = GetById(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Balance = model.Balance;
        }
    }
}
