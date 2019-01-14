using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class GameViewModel
    {
        public DateTime Date { get; set; }
        public Team HomeTeam { get; set; }
        public string GameType { get; set; }
        public string League { get; set; }
    }
}
