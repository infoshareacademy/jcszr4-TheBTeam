using TheBTeam.BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using TheBTeam.BLL;
using System.Linq;

namespace TheBTeam.BLL.Services
{
    public class TransactionService
    {
        private int incomeCategoryLimit = 100;

        private static readonly List<TransactionDto> _transactions = new List<TransactionDto>();
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

        public List<TransactionDto> GetAll(CategoryOfTransaction category, TypeOfTransaction type, string id= null)
        {
            var transaction = new List<TransactionDto>();

            if (id == null)
                transaction = _transactions;
            else
                transaction=_transactions.Where(t => t.UserDto.Id == id).ToList();
            
            if (type == TypeOfTransaction.All)
                return transaction;

            if (type == TypeOfTransaction.Income && category == CategoryOfTransaction.AllIncome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Income).ToList();

            if (type == TypeOfTransaction.Outcome && category == CategoryOfTransaction.allOutcome)
                return transaction.Where(t => t.Type == TypeOfTransaction.Outcome).ToList();

            if (type == TypeOfTransaction.Income && (int)category < incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).ToList();

            if (type == TypeOfTransaction.Outcome && (int)category > incomeCategoryLimit)
                return transaction.Where(t => t.Category == category).ToList();

            throw new InvalidDataException();

        }

        public List<TransactionDto> AddTransaction(TransactionDto modelT, UserDto userDto)
        {
            modelT.OccurrenceTime = DateTime.Now;
            modelT.UserDto = userDto;
            ApplyTransaction(modelT, userDto);
            _transactions.Add(modelT);
            return _transactions;
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

        public static void ApplyTransaction(TransactionDto transactionDto, UserDto userDto)
        {
            if (transactionDto.Type == TypeOfTransaction.Income)
            {
                userDto.Balance += transactionDto.Amount;
                transactionDto.BalanceAfterTransaction = userDto.Balance;
                return;
            }

            userDto.Balance -= transactionDto.Amount;
            transactionDto.BalanceAfterTransaction = userDto.Balance;
        }
        public List<TransactionDto> SearchTransactionByUser(string id)
        {
            return _transactions.Where(t => t.UserDto.Id == id).ToList();
        }
        public static List<TransactionDto> SearchTransactionByType(TypeOfTransaction type, List<TransactionDto> transactions)
        {
            var transactionsByType = transactions.Where(t => t.Type == type).ToList();
            return transactionsByType;
        }
    }
}