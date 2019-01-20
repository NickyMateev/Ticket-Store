﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class TicketsViewModel
    {
        public int Id { get; set; }
        public int SeatNo { get; set; }
        public decimal Price { get; set; }
        public string Game { get; set; }
    }
}
