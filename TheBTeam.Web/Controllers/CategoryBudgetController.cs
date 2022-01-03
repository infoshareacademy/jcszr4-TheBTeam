using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.Models;

namespace TheBTeam.Web.Controllers
{
    public class CategoryBudgetController : Controller
    {
        private readonly PlannerContext _planerContext;

        public CategoryBudgetController(PlannerContext planerContext)
        {
            _planerContext = planerContext;
        }
        // GET: CategoryBudgetController
        //[HttpGet("Show/{id}")]
        public ActionResult Index(int id)
        {
            var modelDal = _planerContext.CategoryBudgets.Where(x => x.UserId == id).ToList();
            var model = modelDal.Select(CategoryBudgetDto.FromDal);
            var transactions = _planerContext.Transactions.Where(x => x.User.Id == id).ToList();
            var sums = transactions.GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Amount).Sum());

            return View(new UsersBudgetDto() { UserId = id, UserBudgets = model, CategorySums = sums });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBudget(int id, IFormCollection collection)
        {
            foreach (var category in collection)
            {
                //TODO: obsłużyć brakujace
                if (Enum.TryParse<CategoryOfTransaction>(category.Key, out var categoryEnum))
                {
                    var existing = _planerContext.CategoryBudgets
                        .FirstOrDefault(x => x.UserId == id && x.Category == categoryEnum);

                    if (existing == null)
                        existing = new CategoryBudget() { UserId = id, Category = categoryEnum };

                    var stringValue = category.Value.FirstOrDefault();
                    existing.PlanedBudget = string.IsNullOrWhiteSpace(stringValue) ? 0M : decimal.Parse(stringValue);

                    _planerContext.Update(existing);
                }
            }

            _planerContext.SaveChanges();


            return RedirectToAction(nameof(Index), new { id });
        }

    }
}
