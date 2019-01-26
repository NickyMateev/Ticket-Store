using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class HighlightGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public bool IsBanner { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
