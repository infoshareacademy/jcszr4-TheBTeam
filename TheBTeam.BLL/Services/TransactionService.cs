using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using TheBTeam.BLL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheBTeam.BLL.DAL;
using TheBTeam.BLL.DAL.Entities;

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

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.AllIncome)
                return transactions.Where(t => t.Type == TypeOfTransaction.Income).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return transactions.Where(t => t.Type == TypeOfTransaction.Outcome).ToList();

            if (type == TypeOfTransaction.Income && (int)category < IncomeCategoryLimit)
                return transactions.Where(t => t.Category == category).ToList();

            if (type == TypeOfTransaction.Outcome && (int)category > IncomeCategoryLimit)
                return transactions.Where(t => t.Category == category).ToList();

            throw new InvalidDataException();

        }
        public List<TransactionDto> GetByDates(List<TransactionDto> transactions, DateTime from, DateTime to)
        {
            if (to == new DateTime(0001, 01, 01))
                to = DateTime.Now;

            transactions = transactions.Where(t => t.CreatedAt >= from.AddDays(1).AddMinutes(-1) && t.CreatedAt <= to.AddDays(1).AddMinutes(-1)).ToList();

            return transactions;
        }
        public void AddTransaction(TransactionDto model, UserDto userDto, int id)
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
    }
}