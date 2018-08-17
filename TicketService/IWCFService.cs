using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TicketService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService" in both code and config file together.
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        void InsertUser(User u);
        [OperationContract]
        void InsertTicket(Ticket t);
        [OperationContract]
        User FindUser(User u);
        [OperationContract]
        Ticket FindTicketById(int Ticket);
        [OperationContract]
        List<Ticket> DisplayAllTickets();
        [OperationContract]
        void DeleteTicket(Ticket t);
    }
}
