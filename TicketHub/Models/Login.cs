using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;

namespace TicketHub.Models
{
    public class Login
    {
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}