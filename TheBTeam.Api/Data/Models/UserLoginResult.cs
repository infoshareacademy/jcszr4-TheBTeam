using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBTeam.Api.Models
{
    public class UserLoginResult : Entity
    {
        public int UserID { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}