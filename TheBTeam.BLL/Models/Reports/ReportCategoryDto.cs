using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Models.Reports
{
    public class ReportCategoryDto
    {
        public CategoryOfTransaction Category { get; set; }
        public Decimal Amount { get; set; }

    }
}
