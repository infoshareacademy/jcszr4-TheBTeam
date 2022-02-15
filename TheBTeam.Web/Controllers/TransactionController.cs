using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TheBTeam.BLL;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using TheBTeam.BLL.Services;
using TheBTeam.BLL.Models;
using static System.String;
using Microsoft.Extensions.Logging;


namespace TheBTeam.Web.Controllers
{
    public class TransactionController : Controller
    {
        //private TransactionService _transactionRepo;//TODO wez tutaj tranzakcje z usera
        private readonly ILogger<TransactionController> _logger;
        private readonly PlannerContext _plannerContext;
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public TransactionController(PlannerContext plannerContext, ILogger<TransactionController> logger)
        {
            _plannerContext = plannerContext;
            _userService = new UserService(plannerContext);
            _transactionService = new TransactionService(plannerContext);
            _logger = logger;
        }
        // GET: TransactionController
        public ActionResult Index(CategoryOfTransaction category, TypeOfTransaction type, string description, DateTime dateFrom, DateTime dateTo, string sortOrder)
        {
            ViewData["EmailSortParam"] = IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            var transactions = TransactionService.Get(category, type, _plannerContext);
            transactions = _transactionService.FilterByDescription(transactions, description);
            transactions = _transactionService.FilterByDates(transactions, dateFrom, dateTo);
            
            transactions = _transactionService.SortAllTransactions(transactions, sortOrder).ToList();  
            
            return View(new TransactionSearchDto(){Category = category, Transactions = transactions, Type = type, Description = description, DateFrom = dateFrom, DateTo = dateTo });
        }

        public ActionResult EmptyList(CategoryOfTransaction category, TypeOfTransaction type, string description, DateTime dateFrom, DateTime dateTo, string sortOrder)
        {
            return View();
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Getting detail transaction item {Id}", id);
            var findId = _plannerContext.Transactions.Find(id);
            if (findId == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND TRANSACTION ", id);
                return RedirectToAction("EmptyList");
            }

            var model = _transactionService.GetByIdToDto(id);
            model.UserDto = _userService.GetByIdToDto((int)model.UserId);
            return View(model);
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UserTransactions(CategoryOfTransaction category, TypeOfTransaction type, int id, string description, DateTime dateFrom, DateTime dateTo, string sortOrder)
        {
            ViewData["DateSortParam"] = IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewData["AmountSortParam"] = sortOrder == "amount" ? "amount_desc" : "amount";

            var user = _plannerContext.Users.Single(x => x.Id == id);

            var transactions = TransactionService.Get(category, type, _plannerContext, id);

            transactions = _transactionService.FilterByDescription(transactions, description);
            transactions = _transactionService.FilterByDates(transactions, dateFrom, dateTo);

            transactions = _transactionService.SortUserTransaction(transactions, sortOrder).ToList();

            return View(new TransactionSearchDto() { FullName = $"{user.FirstName} {user.LastName}", Transactions = transactions, UserId = id, Description = description, DateFrom = dateFrom, DateTo = dateTo});
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
            var model = _transactionService.GetByIdToDto(id);
            return View(model);
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionDto transactionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(transactionDto);
                }
                _transactionService.Edit(transactionDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddTransaction(int id)
        {
            var user = _userService.GetByIdToDto(id);
            var userFullName = $"{user.FirstName} {user.LastName}";
            ViewBag.FullName = userFullName;

            if (!user.IsActive)
                return RedirectToAction("InActiveUser", new { id });

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

                _transactionService.AddTransaction(modelTransactionDto, id);
                return RedirectToAction("UserTransactions", new { id });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("Getting delete transaction item {Id}", id);
            var findId = _plannerContext.Transactions.Find(id);
            if (findId == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND TRANSACTION ", id);
                return RedirectToAction("EmptyList");
            }
            var model = _transactionService.GetByIdToDto(id);
            model.UserDto = _userService.GetByIdToDto((int)model.UserId);
            return View(model);
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TransactionDto transactionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(transactionDto);
                }
                _transactionService.Delete(transactionDto.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult InActiveUser(int id)
        {
            return View();
        }
    }
}
