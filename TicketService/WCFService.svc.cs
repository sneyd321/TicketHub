using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TicketService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WCFService.svc or WCFService.svc.cs at the Solution Explorer and start debugging.
    public class WCFService : IWCFService
    {
        private TicketHubDBEntities1 te = new TicketHubDBEntities1();

        public void DeleteTicket(Ticket t)
        {


            Ticket ticketToDelete;
            using (var context = new TicketHubDBEntities1())
            {
                ticketToDelete = context.Tickets.Where(tic => tic.TicketId == t.TicketId).FirstOrDefault<Ticket>();
            }

            using (var newContext = new TicketHubDBEntities1())
            {
                newContext.Entry(ticketToDelete).State = System.Data.Entity.EntityState.Deleted;

                newContext.SaveChanges();
            }


        }

        public List<Ticket> DisplayAllTickets()
        {
            return te.Tickets.ToList();
        }

        

        public Ticket FindTicketById(int Ticket)
        {
            Ticket tic = new Ticket();
            try
            {
                return te.Tickets.Where(t => t.TicketId == Ticket).Single();
            }
            catch (Exception ex)
            {
                
            }
            return tic;
        }

        public User FindUser(User u)
        {
            User use = new User();
            try
            {
                return te.Users.Where(user => user.Username == u.Username && user.Password == u.Password).Single();
            }
            catch (Exception ex)
            {

            }
            return use;


        }

        public void InsertTicket(Ticket t)
        {
            te.Tickets.Add(t);
            te.SaveChanges();
        }

        public void InsertUser(User u)
        {
            te.Users.Add(u);
            te.SaveChanges();
        }
    }
}
