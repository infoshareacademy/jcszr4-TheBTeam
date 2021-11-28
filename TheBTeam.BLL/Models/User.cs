using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TheBTeam.BLL.Model
{
    public class User
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter balance")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Provide valid balance")]
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Please enter age!")]
        [Display(Name = "Age")]
       /* [Range(13, 99, ErrorMessage = "Please provide value from range 13-99")]*/
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(25)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name")]
        [StringLength(25)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string Company { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get;  set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }

        [JsonConstructor]//TO DO: check if all inputs of created classes exists

        public User(string email)
        {
            Email = email;
        }

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
            Id = GenerateId();
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

        public string GenerateId()
        {
            var random = new Random();
            var bytes = new byte[12];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
