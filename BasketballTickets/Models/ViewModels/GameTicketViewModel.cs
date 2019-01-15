using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class GameTicketViewModel
    {
        public Game Game { get; set; }
        public Ticket Ticket { get; set; }
    }
}
