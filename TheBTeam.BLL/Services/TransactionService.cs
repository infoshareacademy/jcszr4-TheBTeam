using TheBTeam.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TheBTeam.BLL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private readonly PlannerContext _plannerContext;
        private readonly UserService _userService;
        private const int IncomeCategoryLimit = 100;
        public TransactionService(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
            _userService = new UserService(plannerContext);
        }
        public static List<TransactionDto> Get(CategoryOfTransaction category, TypeOfTransaction type, PlannerContext plannerContext, int id = 0)
        {
            var transactions = new List<TransactionDto>();

            if (id == 0)
                transactions = FromDal(plannerContext);
            else
                transactions = FromDal(plannerContext).Where(t => t.UserDto.Id == id).ToList();

            if (type == TypeOfTransaction.All)
                return transactions;

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.allIncome)
                return transactions.Where(t => t.Type == TypeOfTransaction.Income).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return transactions.Where(t => t.Type == TypeOfTransaction.Outcome).ToList();

            if (type == TypeOfTransaction.Income && (int)category < IncomeCategoryLimit)
                return transactions.Where(t => t.Category == category).ToList();

            if (type == TypeOfTransaction.Outcome && (int)category > IncomeCategoryLimit)
                return transactions.Where(t => t.Category == category).ToList();

            throw new InvalidDataException();

        }

        public User GetUserLogged(HttpContext httpContext)
        {
            var userEmail = httpContext.User.Identity.Name;
            var model = _plannerContext.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            return model;
        }

        public List<TransactionDto> FilterByDates(List<TransactionDto> transactions, DateTime dateFrom, DateTime dateTo)
        {
            if (dateTo == new DateTime(0001, 01, 01))
                dateTo = DateTime.Now;

            transactions = transactions.Where(t => t.Date >= dateFrom.AddDays(1).AddMinutes(-1) && t.Date <= dateTo.AddDays(1).AddMinutes(-1)).ToList();

            return transactions;
        }
        public List<TransactionDto> FilterByDescription(List<TransactionDto> transactions, string description)
        {
            if (description is not null)
                return transactions.Where(t => t.Description.ToLower().Contains(description.ToLower())).ToList();

            return transactions;
        }
        public void AddTransaction(TransactionDto model, int id)
        {
            model.UserId = id;
            var modelDal = Transaction.FromDto(model);
            ApplyTransaction(modelDal);
            _plannerContext.Transactions.Add(modelDal);
            _plannerContext.SaveChanges();
        }
        public void ApplyTransaction(Transaction transaction)
        {
            var user = _plannerContext.Users.First(u => u.Id == transaction.UserId);

            if (transaction.Type == TypeOfTransaction.Income)
            {
                user.Balance += transaction.Amount;
                transaction.BalanceAfterTransaction = user.Balance;
                return;
            }

            user.Balance -= transaction.Amount;
            transaction.BalanceAfterTransaction = user.Balance;
            _plannerContext.SaveChanges();
        }
        public TransactionDto GetByIdToDto(int id)
        {
            var modelDal = _plannerContext.Transactions.First(x => x.Id == id);
            modelDal.User = _plannerContext.Users.First(x => x.Id == modelDal.UserId);
            var model = TransactionDto.FromDal(modelDal);
            return model;
        }
        public static List<TransactionDto> FromDal(PlannerContext plannerContext)
        {
            var modelDal = plannerContext.Transactions
                .Include(x => x.User).ToList();
            var model = modelDal.Select(TransactionDto.FromDal).ToList();
            return model;
        }
        public void Edit(TransactionDto transactionDto)
        {
            var transaction = _plannerContext.Transactions.Single(t => t.Id == transactionDto.Id);

            var oldAmount = transaction.Amount;

            transaction.Amount = transactionDto.Amount;
            transaction.Category = transactionDto.Category;
            transaction.Description = transactionDto.Description;
            transaction.Currency = transactionDto.Currency;
            transaction.Date = transactionDto.Date;

            decimal difference = 0.0m;
            if (transaction.Type == TypeOfTransaction.Income)
            {
                transaction.BalanceAfterTransaction += transaction.Amount - oldAmount;
                difference = transaction.Amount - oldAmount;
            }
            else
            {
                transaction.BalanceAfterTransaction -= transaction.Amount - oldAmount;
                difference -= transaction.Amount - oldAmount;
            }

            _userService.EditBalance((int)transaction.UserId, difference);
        }
        public IEnumerable<TransactionDto> SortAllTransactions(IEnumerable<TransactionDto> transactions, string sortOrder)
        {
            switch (sortOrder)
            {
                case "email_desc":
                   return transactions = transactions.OrderByDescending(x => x.UserDto.Email).ThenByDescending(x => x.Date).ThenBy(x=>x.Type).ThenBy(x=>x.Category).ToList();
                case "Date":
                   return transactions = transactions.OrderBy(x => x.Date).ThenByDescending(x => x.UserDto.Email).ThenBy(x => x.Type).ThenBy(x => x.Category).ToList();
                case "date_desc":
                   return transactions = transactions.OrderByDescending(x => x.Date).ThenBy(x => x.Type).ThenBy(x => x.Category).ToList();
                default:
                    return  transactions.OrderBy(x => x.UserDto.Email).ThenByDescending(x => x.Date).ThenBy(x => x.Type).ThenBy(x => x.Category).ToList();
            }
        }
        public IEnumerable<TransactionDto> SortUserTransaction(IEnumerable<TransactionDto> transactions, string sortOrder)
        {
            switch (sortOrder)
            {
                case "date":
                    return transactions = transactions.OrderBy(x => x.Date).ThenBy(x => x.Type).ThenBy(x => x.Category).ThenByDescending(x => x.Amount).ToList();
                case "amount_desc":
                    return transactions.OrderByDescending(x => x.Amount).ThenBy(x => x.Date).ThenBy(x => x.Type)
                        .ThenBy(x => x.Category).ToList();
                case "amount":
                    return transactions.OrderBy(x => x.Amount).ThenBy(x => x.Date).ThenBy(x => x.Type)
                        .ThenBy(x => x.Category).ToList();
                default:
                    return transactions = transactions.OrderByDescending(x => x.Date).ThenBy(x => x.Type).ThenBy(x => x.Category).ThenByDescending(x => x.Amount).ToList();
            }
            }
        public void Delete(int id)
        {
            var transaction = _plannerContext.Transactions.Single(t => t.Id == id);

            if(transaction.Type==TypeOfTransaction.Income)
                _userService.EditBalance((int)transaction.UserId, -transaction.Amount);
            else
                _userService.EditBalance((int)transaction.UserId, transaction.Amount);

            _plannerContext.Remove(transaction);
            _plannerContext.SaveChanges();
        }
        public IEnumerable GroupTransactionForTrending(int id, CategoryOfTransaction category, DateTime dateFrom, DateTime dateTo)
        {
            var transactions = _plannerContext.Transactions.Where(x => x.UserId == id)
                .Where(x => x.Date > dateFrom && x.Date < dateTo).Where(x => x.Category == category).ToList();

            var groupedTransactions = transactions.Select(x => new { x.Date.Year, x.Date.Month, x.Amount })
                .GroupBy(y => new { y.Year, y.Month },
                    (key, group) => new { year = key.Year, month = key.Month, sum = @group.Sum(x => x.Amount) })
                .OrderBy(d => d.year).ThenBy(d => d.month)
                .GroupBy(x=> new{x.year, x.month, x.sum}, (key, group) => new { date = $"{key.month}.{key.year}", sum=key.sum})
                .ToList();
            return groupedTransactions;
        }
    }
}