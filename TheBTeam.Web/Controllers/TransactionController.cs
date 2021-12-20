using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.DAL.Repository;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Models;


namespace TheBTeam.Web.Controllers
{
    public class TransactionController : Controller
    {
        //private TransactionService _transactionRepo;//TODO wez tutaj tranzakcje z usera

        private IRepository<Transaction> _transactionRepo;
        public TransactionController(IRepository<Transaction> transactionRepo)
        {
            _transactionRepo = transactionRepo;
            //_transactionRepo = new TransactionService();
        }
        // GET: TransactionController
        public ActionResult Index(CategoryOfTransaction category, TypeOfTransaction type)
        {
            //TODO how to get here users!
            var model = _transactionRepo.GetAll(/*category, type*/);
            return View(model);
        }
        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UserTransactions(CategoryOfTransaction category, TypeOfTransaction type)
        {
            
            var id = TempData["id"] as string;
            //var model = _transactionService.SearchTransactionByUser(id);
            var model = _transactionRepo.GetAll(/*category, type, id*/);
            TempData["id"] = id;
            return View(model);
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionController/Delete/5
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
