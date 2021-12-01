using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;


namespace TheBTeam.BLL.Services
{
    public class UserService
    {
        private static List<User> _users = LoadDataFromFile.ReadUserFile();
        public List<User> GetAll()
        {
            return _users;
        }
        public User GetById(string id)
        {
            return _users.SingleOrDefault(m => m.Id == id);
        }
        public void Create(User model)
        {
            model.Id = GetNextId();
            model.Registered = DateTime.Now;

            _users.Add(model);
        }
        public string GetNextId()
        {
            if (!_users.Any())
                return String.Empty;
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public void Delete(string id)
        {
            var user = GetById(id);
            _users.Remove(user);
        }

        public void Update(User model)
        {
            var user = GetById(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Balance = model.Balance;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
        }
    }
}
