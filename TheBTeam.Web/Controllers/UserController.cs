using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Models;
using TheBTeam.BLL.Services;

namespace TheBTeam.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly PlannerContext _plannerContext;
        private UserService _userService;
        private TransactionService _transactionService;
        private readonly ApplicationDbContext _applicationDbContext;
        public UserController(PlannerContext plannerContext, ILogger<UserController> logger, ApplicationDbContext applicationDbContext)
        {
            _plannerContext = plannerContext;
            _userService = new UserService(plannerContext);
            _transactionService = new TransactionService(plannerContext);
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        // GET: UserController
        public ActionResult Index()
        {
            var userClaims = _applicationDbContext.UserClaims.Count();
            if (userClaims > 0)
            {
                var lastLoggedClaim = _applicationDbContext.UserClaims.Find(userClaims);
                var lastLoggedRole = lastLoggedClaim.ClaimValue;
                var id = lastLoggedClaim.UserId;

                var usersRegistered = _applicationDbContext.Users;
                var lastLoggedUser = usersRegistered.Where(u => u.Id == id).FirstOrDefault();

                ViewBag.Role = $"{lastLoggedUser.UserName} { lastLoggedRole}";
            }

            var modelDal = _plannerContext.Users.ToList();
            var model= modelDal.Select(UserDto.FromDAL);
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Getting detail user item {Id}", id);
            var findId = _plannerContext.Users.Find(id);
            if (findId == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND USER ", id);
                return RedirectToAction("EmptyList");
            }
            var model = _userService.GetByIdToDto(id);
            return View(model);
        }

        public ActionResult EmptyList()
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                _userService.Create(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       // [HttpPost]
        public ActionResult UserTransactions(int id)
        {
           return RedirectToAction("UserTransactions", "Transaction", new {id});
        }


        // GET: UserController/AddTransaction
       



        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _userService.GetByIdToDto(id);
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _userService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("Getting delete user item {Id}", id);
            var findId = _plannerContext.Users.Find(id);
            if (findId == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND USER ", id);
                return RedirectToAction("EmptyList");
            }
            var model = _userService.GetByIdToDto(id);
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserDto model)
        {
            try
            {
                _userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
