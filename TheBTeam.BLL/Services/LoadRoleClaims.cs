using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.Enums;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.Services
{
    public class LoadRoleClaims
    {
        public async Task<string> SetRoleClaim(UserLoginDto model, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var info = "Role!";
            if (model.Email == "mm@wp.pl")//nadanie admina po emailu
            {
                await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                info = UserRole.Admin.ToString();
            }
            else
            {
                await _userManager.AddClaimAsync(user, new Claim("UserRole", "User"));
                info = UserRole.User.ToString();
            }
            return info;
        }

        public string GetLastRoleClaim(ApplicationDbContext applicationDbContext)
        {
            var userClaims = applicationDbContext.UserClaims.Count();
            if (userClaims > 0)
            {
                var lastLoggedClaim = applicationDbContext.UserClaims.Find(userClaims);
                var lastLoggedRole = lastLoggedClaim.ClaimValue;
                var id = lastLoggedClaim.UserId;
                var usersRegistered = applicationDbContext.Users;
                var lastLoggedUser = usersRegistered.Where(u => u.Id == id).FirstOrDefault();
                return $"{lastLoggedRole}";
            }
            return UserRole.User.ToString();
        }
    }
}
