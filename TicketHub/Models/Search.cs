using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketHub.Models
{
    public class Search
    {
        [Required(ErrorMessage = "Please enter an Ticket Id")]
        public int Id
        {
            get;
            set;
        }
    }
}