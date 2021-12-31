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
        private const int IncomeCategoryLimit = 100;

        private static readonly List<TransactionDto> _transactions = new List<TransactionDto>();

        public TransactionService(PlannerContext plannerContext)
        {
            _plannerContext = plannerContext;
        }
        //private list<transaction> transaction = new list<transaction>
        //{
        //    new transaction
        //    {
        //        user = new user("mm@wp.pl"),
        //        type = typeoftransaction.income,
        //        category = categoryoftransaction.salary,
        //        currency = currency.pln,
        //        amount = 1220m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.income,1220m)
        //    },
        //    new transaction
        //    {
        //        user = new user("mariobros@wp.pl"),
        //        type = typeoftransaction.income,
        //        category = categoryoftransaction.salary,
        //        currency = currency.pln,
        //        amount = 1520m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.income,1520m)
        //    },
        //    new transaction
        //    {
        //        user = new user("grzesio@wp.pl"),
        //        type = typeoftransaction.outcome,
        //        category = categoryoftransaction.credit,
        //        currency = currency.pln,
        //        amount = 2520m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.outcome,2520m)
        //    },
        //    new transaction
        //    {
        //        user = new user("edekwielki@wp.pl"),
        //        type = typeoftransaction.outcome,
        //        category = categoryoftransaction.car,
        //        currency = currency.pln,
        //        amount = 1250m,
        //        balanceaftertransaction = getbalanceaftertransaction(typeoftransaction.outcome,1250m)
        //    }
        //};

        public static List<TransactionDto> GetAll(CategoryOfTransaction category, TypeOfTransaction type, PlannerContext plannerContext, int id = 0)
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

        public void AddTransaction(TransactionDto model, UserDto userDto, int id)
        {
            model.UserId = id;
            var modelDal = Transaction.FromDto(model);
            ApplyTransaction(modelDal);//TODO: make it works on DAL
            _plannerContext.Transactions.Add(modelDal);
            _plannerContext.SaveChanges();
        }

        public List<TransactionDto> GetTransactionFromUser()
        {
            return _transactions;
        }


        public static decimal GetBalanceAfterTransaction(TypeOfTransaction typeOfTransaction, decimal amount)
        {
            decimal result = 0;
            if (typeOfTransaction == TypeOfTransaction.Income)
            {
                result += amount;
                return result;
            }
            result -= amount;
            return result;
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
        public List<TransactionDto> SearchTransactionByUser(int id)
        {
            return _transactions.Where(t => t.UserDto.Id == id).ToList();
        }
        public static List<TransactionDto> SearchTransactionByType(TypeOfTransaction type, List<TransactionDto> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }

        public static List<TransactionDto> FromDal(PlannerContext plannerContext)
        {
            var modelDal = plannerContext.Transactions
                .Include(x => x.User).ToList();
            var model = modelDal.Select(TransactionDto.FromDal).ToList();
            return model;
        }
    }
}