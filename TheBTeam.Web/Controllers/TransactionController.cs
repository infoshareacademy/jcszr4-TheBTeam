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
        public ActionResult Details(string id)
        {
            var model = _transactionService.GetTransactionByEmail(id);
            return View(model);
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UserTransactions()
        {
            
            var id = TempData["id"] as string;
            var model = _transactionService.SearchTransactionByUser(id);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: TransactionController/Create
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

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionController/Edit/5
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

        // GET: TransactionController/Delete/5
        public ActionResult Delete()
        {

            var id = TempData["id"] as string;
            var model = _transactionService.GetTransactionByUser(id);
            return View(model);
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                //here id is key as user.Email
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
