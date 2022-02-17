using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }
}
