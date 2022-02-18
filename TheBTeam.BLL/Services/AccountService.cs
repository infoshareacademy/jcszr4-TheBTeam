using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using PasswordVerificationResult = Microsoft.AspNet.Identity.PasswordVerificationResult;

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
            var decodeBasePassword = Base64EncodeDecode.Base64Decode(user.PasswordHash);

            if (user != null && password == decodeBasePassword)
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
