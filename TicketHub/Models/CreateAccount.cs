using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHub.Models
{
    public class CreateAccount
    {
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please enter your username")]
        public string Username
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please enter your phone")]
        public string Phone
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Please enter your email")]
        public string Email
        {
            get;
            set;
        }
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8)]
        [Required(ErrorMessage = "Please enter your password")]
        public string NewPassword
        {
            get;
            set;
        }
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required(ErrorMessage = "Please enter your password")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}