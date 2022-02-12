using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly PlannerContext _plannerContext;

        public AccountService(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }

        public async Task<LoginResult> ValidateUser(string userName, string password)
        {
            //var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userName);
            var user = await _plannerContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userName);

            if (user != null & user.Role != null)
            {
                return new LoginResult { UserName = userName, RoleName = user.Role.Name, Success = true };
            }

            return new LoginResult { Success = false };
        }
    }
}
