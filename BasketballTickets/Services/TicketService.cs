using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Services
{
    public class TicketService
    {
        public static ICollection<Ticket> GenerateTickets(int gameId, decimal price, int amount)
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

        public static decimal GetGamePrice(DbSet<Ticket> tickets, int gameId)
        {
            return tickets.Where(t => t.GameId == gameId).First().Price;
        }
    }
}
