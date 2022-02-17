using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.Services
{
    public interface IAccountService
    {
        Task<LoginResult> ValidateUser(string userName, string password);
    }
}
