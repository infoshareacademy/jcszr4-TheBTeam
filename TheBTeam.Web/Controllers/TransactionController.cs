using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Models;


namespace TheBTeam.Web.Controllers
{
    public class TransactionController : Controller
    {
        //private TransactionService _transactionRepo;//TODO wez tutaj tranzakcje z usera

        private readonly PlannerContext _plannerContext;
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public TransactionController(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
            _userService = new UserService(plannerContext);
            _transactionService = new TransactionService(plannerContext);
        }
        // GET: TransactionController
        public ActionResult Index(CategoryOfTransaction category, TypeOfTransaction type)
        {
            var modelDal = _plannerContext.Transactions
                .Include(x=> x.User).ToList();
            var model = modelDal.Select(TransactionDto.FromDal);

            //var model = _transactionRepo.GetAll(/*category, type*/);
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

        public ActionResult UserTransactions(CategoryOfTransaction category, TypeOfTransaction type, int id)
        {
            var model = TransactionService.GetAll(category, type, _plannerContext, id);
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

        public ActionResult AddTransaction()
        {
            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransaction(TransactionDto modelTransactionDto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(modelTransactionDto);
                }
                var user = _userService.GetByIdToDto(id);
                _transactionService.AddTransaction(modelTransactionDto, user, id);
                return RedirectToAction("UserTransactions", new {id});
            }
            catch (Exception e)
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
