using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;
using Microsoft.AspNetCore.Authorization;


namespace TheBTeam.Web.Controllers
{
    public class CategoryBudgetController : Controller
    {
        private readonly PlannerContext _planerContext;
        private readonly BudgetService _budgetService;
        private readonly TransactionService _transactionService;

        public CategoryBudgetController(PlannerContext planerContext, BudgetService budgetService, TransactionService transactionService)
        {
            _planerContext = planerContext;
            _budgetService = budgetService;
            _transactionService = transactionService;
        }
        // GET: CategoryBudgetController
        //[HttpGet("Show/{id}")]
        public async Task<ActionResult> UserBudget(int id, DateTime date)
        {
            //TODO: .toQuerryString
            var transactionsDal = await _planerContext.CategoryBudgets.Where(x => x.UserId == id).OrderByDescending(x=>x.Date).ToListAsync();
            if (!transactionsDal.Any())
                return RedirectToAction("Create", new { id = id });

            var userFullName =
                $"{_planerContext.Users.Single(x => x.Id == id).FirstName} {_planerContext.Users.Single(x => x.Id == id).LastName}";
            
            transactionsDal = date == default ? transactionsDal.Where(x => x.Date == transactionsDal.First().Date).ToList() 
                : transactionsDal.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month).ToList();
            if (!transactionsDal.Any())
                return RedirectToAction("EmptyList", new{id=id, userFullName=userFullName, date= date});

            var budgets = transactionsDal.Select(CategoryBudgetDto.FromDal).OrderByDescending(x=>x.Date);
            var transactions = _planerContext.Transactions.Where(x => x.User.Id == id).Where(x=>x.Date.Month==budgets.First().Date.Month && x.Date.Year==budgets.First().Date.Year).ToList();
            var sums = transactions.GroupBy(x => x.Category)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Amount).Sum());

            var expenses = (from sum in sums where (int) sum.Key > 100 select new Tuple<string, decimal>(sum.Key.ToString(), sum.Value)).ToList();

            return View(new UsersBudgetDto() { UserId = id, UserBudgets = budgets, CategorySums = sums, UserFullName = userFullName, Date = date, Expenses = expenses});
        }
        public  ActionResult UsersTrending(int id, CategoryOfTransaction category, DateTime dateFrom, DateTime dateTo)
        {
            _budgetService.CheckDateToTrending(ref dateFrom, ref dateTo);

            var groupedTransactions = _transactionService.GroupTransactionForTrending(id, category, dateFrom, dateTo);

            return View(new UsersTrendingDto(){Transactions = groupedTransactions, Category = category, UserId = id});
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

            return RedirectToAction("UserBudget", new {id});
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
                        .FirstOrDefault(x => x.UserId == id && x.Category == categoryEnum) ?? new CategoryBudget() { UserId = id, Category = categoryEnum };

                    var stringValue = category.Value.FirstOrDefault();
                    existing.PlanedBudget = string.IsNullOrWhiteSpace(stringValue) ? 0M : decimal.Parse(stringValue);

                    _planerContext.Update(existing);
                }
            }

            _planerContext.SaveChanges();


            return RedirectToAction(nameof(UserBudget), new { id });
        }
        [Authorize]
        public ActionResult Index()
        {
            var activeBudgets = _planerContext.CategoryBudgets.Where(x => x.PlanedBudget > 0).ToList().Select(CategoryBudgetDto.FromDal);
            var activeUsersId = activeBudgets.Select(x=>x.UserId).Distinct().ToArray();

            var checkRole = this.HttpContext.User.Identity.Name.Contains("admin");
            if (checkRole == false)
            {
                var userEmail = this.HttpContext.User.Identity.Name;
                var findIdUser = _planerContext.Users.Where(u => u.Email == userEmail).Select(u => u.Id).FirstOrDefault();
                activeUsersId = activeBudgets.Where(u => u.UserId == findIdUser).Select(x => x.UserId).Distinct().ToArray();
            }

            var activeUsers = (from id in activeUsersId select _planerContext.Users
                .FirstOrDefault(x => x.Id == id) into user where user is not null select UserDto.FromDAL(user));

            var model = activeUsersId
                .Select(userId => new IndexBudget {User = activeUsers.First(x => x.Id == userId), CategoryBudget = activeBudgets
                    .Where(x => x.UserId == userId), Transactions = _planerContext.Transactions.Where(x=>x.UserId==userId).AsEnumerable().Select(TransactionDto.FromDal).ToList()}).ToList();

            return View(model);
        }

    }
}
