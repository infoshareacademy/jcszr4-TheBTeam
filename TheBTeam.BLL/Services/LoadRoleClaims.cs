using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<string> SetRoleClaimFromJson(UserLoginDto model, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            var admins = ReadAdminEmailsFromJson();
            var info = "Role!";
            var isAdmin = admins.Any(a => a.Email == model.Email);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (isAdmin == true && user != null)
            {
                foreach (var admin in admins)
                {
                    user = await _userManager.FindByEmailAsync(admin.Email);
                    if (admin.Email == model.Email)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                        info = UserRole.Admin.ToString();
                    }
                 }
            }
            else
            {
                if (user != null)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "User"));
                    info = UserRole.User.ToString();
                }
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

        private IList<AdminEmails> ReadAdminEmailsFromJson()
        {
            string fileName = @"SourceFiles\admins.json";
            string jsonString = File.ReadAllText(fileName);
            List<AdminEmails> playerData = JsonConvert.DeserializeObject<List<AdminEmails>>(jsonString);
            return playerData;
        }
    }
}
