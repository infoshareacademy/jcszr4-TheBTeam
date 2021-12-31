using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;


namespace TheBTeam.BLL.Services
{
    public class UserService
    {
        private readonly PlannerContext _plannerContext;
        private static readonly List<UserDto> Users = LoadDataFromFile.ReadUserFile();

        public UserService(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public UserDto GetById(int id)
        {
            return Users.SingleOrDefault(m => m.Id == id);
        }
        public void Create(UserDto model)
        {
            var modelDal = User.FromDto(model);
            _plannerContext.Users.Add(modelDal);
            _plannerContext.SaveChanges();
        }

        public UserDto GetByIdToDto(int id)
        {
            var modelDal = _plannerContext.Users.First(x => x.Id == id);
            var model = UserDto.FromDAL(modelDal);
            return model;
        }


        public void Delete(int id)
        {
            var user = _plannerContext.Users.SingleOrDefault(u => u.Id == id);

            _plannerContext.Remove(user);
            _plannerContext.SaveChanges();
        }

        public void Update(UserDto model)
        {
            var user = _plannerContext.Users.SingleOrDefault(u => u.Id == model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Balance = model.Balance;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.Company = model.Company;
            user.Address = model.Address;
            user.Phone = model.Phone;

            _plannerContext.SaveChanges();
        }
    }
}
