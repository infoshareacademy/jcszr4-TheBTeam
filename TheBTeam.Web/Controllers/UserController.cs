using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL.Model;
using TheBTeam.BLL.Services;

namespace TheBTeam.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;
        private TransactionService _transactionService;
        public UserController()
        {
            _userService = new UserService();
            _transactionService = new TransactionService();
        }

        // GET: UserController
        public ActionResult Index()
        {

            var model = _userService.GetAll();
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            var model = _userService.GetById(id);
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
        public ActionResult Create(User model)
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
        public ActionResult AddTransaction(Transaction modelTransaction, string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(modelTransaction);
                }
                var user = _userService.GetById(id);
                _transactionService.AddTransaction(modelTransaction, user);
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
        public ActionResult Edit(string id, User model)
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
        public ActionResult Delete(string id, User model)
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
