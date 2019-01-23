using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SeatNo { get; set; }
        public decimal Price { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
