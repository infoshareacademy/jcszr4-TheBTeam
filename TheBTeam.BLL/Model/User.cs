using System;
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
        public int Age { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Gender { get; private set; }
        public string Company { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public DateTime Registered { get;  }
        //public User(){}
        [JsonConstructor]
        public User(string id, decimal balance, string currency, int age, string firstName, string lastName, string gender, string company, string email, string phone, string address)
        {
            Id = id;
            Balance = balance;
            Currency = new Currency(currency);
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
        public User(decimal balance, string currency, int age, string firstName, string lastName, string gender, string company, string email, string phone, string address)
        {
            Id = GenerateId();
            Balance = balance;
            Currency = new Currency(currency);
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
        public void DeactivateUser()
        {
            IsActive = false;
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
