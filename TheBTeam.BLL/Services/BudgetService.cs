using System;

namespace TheBTeam.BLL.Services
{
    public class BudgetService
    {
        public void CheckDateToTrending(ref DateTime dateFrom, ref DateTime dateTo)
        {
            var now = DateTime.UtcNow;
            var currentMonth = new DateTime(now.Year, now.Month, 1);
            var pastMonth = currentMonth.AddMonths(-2).AddDays(-1);
            dateFrom = dateFrom == default ? new DateTime(now.Year, now.Month, 1).AddMonths(-3) : dateFrom;
            dateTo = dateTo == default ? DateTime.UtcNow : dateTo;
        }
    }
}