using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace The_Blob.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"\S+@(\S+\.)+\w{2,4}", ErrorMessage = "There is a problem with the email")]
        [Required(ErrorMessage = "You must enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must enter a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must enter a password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        public void CreateUser()
        {

        }
        public void EditUser()
        {

        }
        public void RemoveUser()
        {

        }
    }
}
