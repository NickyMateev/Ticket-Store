using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class CategorizedGamesViewModel
    {
        public string GamesType { get; set; }
        public ICollection<GameViewModel> Games { get; set; }
    }
}
