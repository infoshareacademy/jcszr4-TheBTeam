using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter balance")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Provide valid balance")]
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Please enter age!")]
        [Display(Name = "Age")]
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
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public static UserDto FromDAL(User user)
        {
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Balance = user.Balance,
                Currency = user.Currency,
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Company = user.Company,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
