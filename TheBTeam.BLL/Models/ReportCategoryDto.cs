using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBTeam.BLL.Models.Reports
{
    public class ReportCategoryDto
    {
        public int Id { get; set; }
        public CategoryOfTransaction Category { get; set; }
        public Decimal Amount { get; set; }

    }
}
