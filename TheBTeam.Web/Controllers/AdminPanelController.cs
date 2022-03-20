using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;
using TheBTeam.Web.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using TheBTeam.BLL;

namespace TheBTeam.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly PlannerContext _plannerContext;
        private UserService _userService;
        private TransactionService _transactionService;
        private CategoryLogService _categoryLogService;
        public AdminPanelController(PlannerContext plannerContext, ILogger<UserController> logger,IConfiguration configuration)
        {
            _plannerContext = plannerContext;
            _userService = new UserService(plannerContext);
            _transactionService = new TransactionService(plannerContext);
            _logger = logger;
            _categoryLogService = new CategoryLogService(configuration);
        }

        public IActionResult Index()
        {
         
            var transactions = TransactionService.FromDal(_plannerContext);
            var possibleOutcomeCategories = transactions.Select(x => x.Category).Distinct();
            var dict = new Dictionary<CategoryOfTransaction, int>();
            foreach(CategoryOfTransaction item in possibleOutcomeCategories)
            {
                dict.Add(item, transactions.Count(x => x.Category == item));
            }
            var modelDal = _userService.GetAllUsersAccordingRole(HttpContext);
            var model = modelDal.Select(UserDto.FromDAL);
            var Data =_categoryLogService.GetReport();
            int userAmount = model.Count();
            int transactionAmount = transactions.Count();
            int activeUserAmount = model.Count(x => x.IsActive == true);
            Decimal totalVolume = transactions.Sum(x => x.Amount);

            return View(new AdminPanelDto() { ActiveUserAmount=activeUserAmount,UserAmount=userAmount,TransactionAmount=transactionAmount});

        }
        public IActionResult ReportPage()
        {

            var transactions = TransactionService.FromDal(_plannerContext);
            var possibleOutcomeCategories = transactions.Select(x => x.Category).Distinct();
            var dict = new Dictionary<CategoryOfTransaction, int>();
            foreach (CategoryOfTransaction item in possibleOutcomeCategories)
            {
                dict.Add(item, transactions.Count(x => x.Category == item));
            }
            var modelDal = _userService.GetAllUsersAccordingRole(HttpContext);
            var model = modelDal.Select(UserDto.FromDAL);
            var Data = _categoryLogService.GetReport();
            int userAmount = model.Count();
            int transactionAmount = transactions.Count();
            int activeUserAmount = model.Count(x => x.IsActive == true);
            Decimal totalVolume = transactions.Sum(x => x.Amount);

            return View(new AdminPanelDto() { ActiveUserAmount = activeUserAmount, UserAmount = userAmount, TransactionAmount = transactionAmount }) ;

        }
        public ActionResult PartialUserIndex()
        {
            var modelDal = _userService.GetAllUsersAccordingRole(HttpContext);
            var model = modelDal.Select(UserDto.FromDAL);

            return PartialView("_PartialUserIndex", model);
        }
        // GET: AdminController/Details/5
        public ActionResult ApiTest()
        {
            var model = _categoryLogService.GetReport().Result;
            return View(model);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
