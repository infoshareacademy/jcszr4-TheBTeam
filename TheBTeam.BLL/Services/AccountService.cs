using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly PlannerContext _plannerContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(PlannerContext plannerContext, IPasswordHasher<User> passwordHasher)
        {
            _plannerContext = plannerContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResult> ValidateUser(string userName, string password)
        {
             var user = await _plannerContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userName);

            var hashedPassword = this._passwordHasher.HashPassword(user, password);

            if (user != null && user.PasswordHash.Equals(user.PasswordHash, StringComparison.Ordinal))
            {
                if (user.Role != null)
                {
                    return new LoginResult { UserName = userName, RoleName = user.Role.Name, Success = true };
                }
            }

            return new LoginResult { Success = false };
        }
    }
}
