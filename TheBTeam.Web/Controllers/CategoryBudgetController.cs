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
using TheBTeam.BLL.DAL.Entities;
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
            var userFullName =
                $"{_planerContext.Users.First(x => x.Id == id).FirstName} {_planerContext.Users.First(x => x.Id == id).LastName}";

            if (!modelDal.Any())
                return RedirectToAction("Create", new {id=id});

            if (date.Year == 1)
                modelDal = modelDal.Where(x => x.Date == modelDal.First().Date).ToList();
            else
                modelDal = modelDal.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month).ToList();

            if (!modelDal.Any())
                return RedirectToAction("EmptyList", new{id=id, userFullName=userFullName, date= date});

            var model = modelDal.Select(CategoryBudgetDto.FromDal).OrderByDescending(x=>x.Date);
            var transactions = _planerContext.Transactions.Where(x => x.User.Id == id).Where(x=>x.WhenMade.Month==model.First().Date.Month && x.WhenMade.Year==model.First().Date.Year).ToList();
            var sums = transactions.GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Amount).Sum());
            

            ViewBag.Date = date;

            return View(new UsersBudgetDto() { UserId = id, UserBudgets = model, CategorySums = sums, UserFullName = userFullName});
        }

        public ActionResult EmptyList(int id, string userFullName, DateTime date)
        {
            ViewBag.Id = id;
            ViewBag.UserFullName = userFullName;
            ViewBag.Date = date;
            return View();
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

        public ActionResult Index()
        {
            var activeBudgets = _planerContext.CategoryBudgets.Where(x => x.PlanedBudget > 0).ToList().Select(CategoryBudgetDto.FromDal);
            var activeUsersId = activeBudgets.Select(x=>x.UserId).Distinct().ToArray();

            var activeUsers = (from id in activeUsersId select _planerContext.Users
                .FirstOrDefault(x => x.Id == id) into user where user is not null select UserDto.FromDAL(user));

            var model = activeUsersId
                .Select(userId => new IndexBudget {User = activeUsers.First(x => x.Id == userId), CategoryBudget = activeBudgets
                    .Where(x => x.UserId == userId)}).ToList();

            return View(model);
        }

    }
}
