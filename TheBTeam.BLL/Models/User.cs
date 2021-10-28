﻿using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace TheBTeam.BLL.Model
{
    public class User
    {
        public string Id { get; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public int Age { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; set; }
        public Gender Gender { get;  set; }
        public string Company { get; set; }
        public string Email { get; private set; }
        public string Phone { get; set; }
        public string Address { get;  set; }
        public DateTime Registered { get; }

        [JsonConstructor]//TO DO: check if all inputs of created classes exists
        public User(string id, decimal balance, Currency currency, int age, string firstName, string lastName, Gender gender, string company, string email, string phone, string address)
        {
            Id = id;
            Balance = balance;
            Currency = currency;
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Company = company;
            Email = email;
            Phone = phone;
            Address = address;
            Registered = DateAndTime.Now;
        }
        public User(string firstName, string lastName, Gender gender, int age, string email, string phone, string address, string company, Currency currency, decimal balance = 0)
        {
            Id = GenerateId();//TODO check if 
            Balance = balance;
            //Currency = new Currency(currency);
            Currency = currency;
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Company = company;
            Email = email;
            Phone = phone;
            Address = address;
            Registered = DateAndTime.Now;
        }

        private string GenerateId()
        {
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
