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
        public ActionResult UserBudget(int id, DateTime date)
        {
            var modelDal = _planerContext.CategoryBudgets.Where(x => x.UserId == id).ToList().OrderByDescending(x=>x.Date).ToList();

            if (modelDal.Count == 0)
                return RedirectToAction("Create", new {id=id});

            //if(date.Year==1)
            //    date.Year=

            var model = modelDal.Select(CategoryBudgetDto.FromDal);
            var transactions = _planerContext.Transactions.Where(x => x.User.Id == id).ToList();
            var sums = transactions.GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Amount).Sum());
            var userFullName =
                $"{_planerContext.Users.First(x => x.Id == id).FirstName} {_planerContext.Users.First(x => x.Id == id).LastName}";

            return View(new UsersBudgetDto() { UserId = id, UserBudgets = model, CategorySums = sums, UserFullName = userFullName});
        }

        public ActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, DateTime date)
        {
            foreach (var category in Enum.GetNames(typeof(CategoryOfTransaction)))
            {
                Enum.TryParse<CategoryOfTransaction>(category, out var categoryEnum);
                var categoryBudget = new CategoryBudget() {Category = categoryEnum, UserId = id, Date = date};
                _planerContext.Add(categoryBudget);
            }

            _planerContext.SaveChanges();

            return RedirectToAction("UserBudget", new {id=id});
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


            return RedirectToAction(nameof(UserBudget), new { id });
        }

    }
}
