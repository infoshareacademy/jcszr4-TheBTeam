using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;

namespace TheBTeam.BLL
{
    public class User
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }
        public User(string id , bool isActive, decimal balance,string currency,int age,string firstname, string lastname, string gender, string company, string email, string phone, string adress, DateTime registered)
        {
            Id = Id;
            IsActive = IsActive;
            Balance = balance;
            Currency = currency;
            Age = age;
            FirstName = firstname;
            LastName = lastname;
            Gender = gender;
            Company = company;
            Email = email;
            Phone = phone;
            Address = adress;
            Registered = DateAndTime.Now;
        }

    }
}
