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
        public static ICollection<Ticket> GenerateTickets(Game game, int capacity)
        {
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 1; i <= capacity; i++)
            {
                if (game.Tickets == null || game.Tickets.Where(t => t.SeatNo == i).Count() == 0)
                {
                    tickets.Add(
                        new Ticket
                        {
                            SeatNo = i,
                            Price = game.TicketPrice
                        }
                    );
                }
            }

            return tickets;
        }


        public static decimal GetGamePrice(DbSet<Ticket> tickets, int gameId)
        {
            return tickets.Where(t => t.GameId == gameId).First().Price;
        }
    }
}
