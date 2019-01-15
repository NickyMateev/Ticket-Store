using BasketballTickets.Data;
using BasketballTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Services
{
    public class TicketService
    {
        public static ICollection<Ticket> generateTickets(int gameId, decimal price, int amount)
        {
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 1; i <= amount; i++)
            {
                tickets.Add(
                    new Ticket
                    {
                        SeatNo = i,
                        Price = price
                    });
            }

            return tickets;
        }
    }
}
