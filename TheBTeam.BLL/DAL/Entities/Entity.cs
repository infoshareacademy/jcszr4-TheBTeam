using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.DAL.Entities
{
    public class Entity
    {
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
