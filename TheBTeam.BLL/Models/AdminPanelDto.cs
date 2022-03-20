using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Models
{
    [Keyless]
    public class AdminPanelDto
    {

       public int UserAmount { get; set; }
       public int ActiveUserAmount { get; set; }
       public int TransactionAmount { get; set; }
    }
}
