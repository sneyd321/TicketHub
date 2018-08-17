using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHub.Models
{
    public class Ticket
    {
        [Required(ErrorMessage = "Please enter a section")]
        public string Section { get; set; }
        [Required(ErrorMessage = "Please enter a row")]
        public string Row { get; set; }
        [Required(ErrorMessage = "Please enter a seat")]
        public string Seat { get; set; }
        [Required(ErrorMessage = "Please enter a type of admission")]
        public string Admission { get; set; }
        [Required(ErrorMessage = "Please enter the name of the event")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the date of the event")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter the price of the ticket")]
        public string Price { get; set; }


    }
}