using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketHub.Models;
using TicketHub.ServiceReference1;


namespace TicketHub.Controllers
{
    
    public class HomeController : Controller
    {
        
        private WCFServiceClient client = new WCFServiceClient();
        public ActionResult Index()
        {
            return View("LandingPage");
        }
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            string loginPressed = Request.Form["submit"];
            
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Username = login.Username;
                user.Password = login.Password;
                if (loginPressed == "true")
                {
                    if (client.FindUser(user).FirstName != null)
                    {
                        ViewBag.ListTickets = client.DisplayAllTickets();
                        return View("Ticket");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username/Password incorrect");
                    }
                    
                }
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(CreateAccount account)
        {
            User user = new User();
            string createAccount = Request.Form["submit"];
      
            if (ModelState.IsValid)
            {
                user.FirstName = account.FirstName;
                user.LastName = account.LastName;
                user.Username = account.Username;
                user.Address = account.Address;
                user.Phone = account.Phone;
                user.Email = account.Email;
                user.Password = account.NewPassword;
                client.InsertUser(user);
                if (createAccount == "true")
                {
                    return View("LandingPage");
                }
            }

            return View();
        }
        [HttpGet]
        public ViewResult Ticket()
        {
            ViewBag.ListTickets = client.DisplayAllTickets();

            return View();
        }
        [HttpPost]
        public ActionResult Ticket(Search s)
        {
            ViewBag.ListTickets = client.DisplayAllTickets();
            return View();

        }
        [HttpGet]
        public ActionResult BuyTicket()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BuyTicket(Search search)
        {
            var response = Request["g-recaptcha-response"];

            ServiceReference1.Ticket ticket = new ServiceReference1.Ticket();

            ticket = client.FindTicketById(search.Id);
            if (ticket.Name == null)
            {
                response = "";
                ViewBag.Error = $"No ticket with ID: {search.Id}";
            }

            if (ModelState.IsValid && response != "")
            {
               
                
               
                ViewBag.Id = ticket.TicketId;
                ViewBag.Section = ticket.Section;
                ViewBag.Row = ticket.Row;
                ViewBag.Seat = ticket.Seat;
                ViewBag.Admission = ticket.Admission;
                ViewBag.Name = ticket.Name;
                ViewBag.Date = ticket.Date;
                ViewBag.Price = ticket.Price;



                //secret that was generated in key value pair
                const string secret = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";

                var webClient = new WebClient();
                var reply =
                    webClient.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                    secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                //when response is false check for the error message
                if (!captchaResponse.Success)
                {
                    if (captchaResponse.ErrorCodes.Count <= 0) return View();

                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            ViewBag.Message = "The secret parameter is missing.";
                            break;
                        case ("invalid-input-secret"):
                            ViewBag.Message = "The secret parameter is invalid or malformed.";
                            break;

                        case ("missing-input-response"):
                            ViewBag.Message = "The response parameter is missing.";
                            break;
                        case ("invalid-input-response"):
                            ViewBag.Message = "The response parameter is invalid or malformed.";
                            break;

                        default:
                            ViewBag.Message = "Error occured. Please try again";
                            break;
                    }
                }
                else
                {
                    ViewBag.Message = "Valid";
                    
                    if (ticket.TicketId == search.Id)
                    {
                        client.DeleteTicket(ticket);
                    }
                    
                    return View("Confirm");
                }
            }
            else
            {
                ModelState.AddModelError("", "Are you a robot?");
            }
            
            

           
            //ViewBag.ListTickets = client.DisplayAllTickets();
            return View();
            
        }
        [HttpGet]
        public ActionResult SellTicket()
        {
            return View();
        }
        [HttpPost]
        public ViewResult SellTicket(Models.Ticket t)
        {
            if (ModelState.IsValid)
            {
                ServiceReference1.Ticket ticket = new ServiceReference1.Ticket();
                ticket.Section = t.Section;
                ticket.Row = t.Row;
                ticket.Seat = t.Seat;
                ticket.Admission = t.Admission;
                ticket.Name = t.Name;
                ticket.Date = t.Date;
                ticket.Price = t.Price;

                client.InsertTicket(ticket);
                ViewBag.ListTickets = client.DisplayAllTickets();
                return View("Ticket");
            }
            return View();
           
        }
        

        


    }
}
    
