using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Models;


namespace TheBTeam.Web.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionService _transactionService;
        private UserService _userService;
        public TransactionController()
        {
            _transactionService = new TransactionService();
            _userService = new UserService();
        }
        // GET: TransactionController
        public ActionResult Index(CategoryOfTransaction category, TypeOfTransaction type)
        {
            var model = _transactionService.GetAll(category, type);
            return View(model);
        }

        // GET: TransactionController/Details/5
        //public ActionResult Details(string id)//
        //{
        //    var model = _transactionService.GetTransactionByEmail(id);
        //    return View(model);
        //}


        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                _transactionService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserTransactions(CategoryOfTransaction category, TypeOfTransaction type)
        {      
            var id = TempData["id"] as string;
            //var model = _transactionService.SearchTransactionByUser(id);
            var model = _transactionService.GetAll(category, type, id);
            TempData["id"] = id;
            return View(model);
        }

        public ActionResult AddTransaction()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransaction(Transaction model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var id = TempData["id"] as string;
                var user = _userService.GetById(id);
                _transactionService.AddTransactionByUser(model, user);

                return RedirectToAction("UserTransactions", "User");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _transactionService.GetById(id);
            return View(model);
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Transaction model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _transactionService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)//why here is 0!!
        {
            var model = _transactionService.GetById(id);
            return View(model);
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Transaction model)
        {
            try
            {
                _transactionService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
