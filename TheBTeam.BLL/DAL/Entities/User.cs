using System;
using System.ComponentModel.DataAnnotations;
using TheBTeam.BLL.Models;

namespace TheBTeam.BLL.DAL.Entities
{
    public class User : Entity
    {
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter balance")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Provide valid balance")]
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Please enter age!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name")]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string Company { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get;  set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
        public string PasswordHash { get; set; }

        public static User FromDto(UserDto userDto)
        {
            if (userDto== null)
            {
                return null;
            }
            var user = new User
            {
                Id = userDto.Id,
                IsActive = userDto.IsActive,
                Balance = userDto.Balance,
                Currency = userDto.Currency,
                Age = userDto.Age,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Gender = userDto.Gender,
                Company = userDto.Company,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
            };

            return user;
        }
    }
}
