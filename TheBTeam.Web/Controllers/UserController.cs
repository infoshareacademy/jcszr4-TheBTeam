using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly PlannerContext _plannerContext;
        private UserService _userService;
        private TransactionService _transactionService;
        public UserController(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
            _userService = new UserService();
            _transactionService = new TransactionService();
        }

        // GET: UserController
        public ActionResult Index()
        {
            var modelDal = _plannerContext.Users.ToList();
            var model= modelDal.Select(UserDto.FromDAL);
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var modelDal = _plannerContext.Users.First(x => x.Id == id);
            var model = UserDto.FromDAL(modelDal);
            //var model = modelDal.Select(UserDto.FromDAL);
            return View(model);
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

        public ActionResult UserTransactions(string id)
        {
            
            TempData["id"] = id;

            return RedirectToAction("UserTransactions", "Transaction");
        }


        // GET: UserController/AddTransaction
        public ActionResult AddTransaction()
        {
            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransaction(TransactionDto modelTransactionDto, string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(modelTransactionDto);
                }
                var user = _userService.GetById(id);
                //_transactionService.AddTransaction(modelTransactionDto, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _userService.GetById(id);
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
        public ActionResult Delete(string id)
        {
            var model = _userService.GetById(id);
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, UserDto model)
        {
            try
            {
                //_userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
