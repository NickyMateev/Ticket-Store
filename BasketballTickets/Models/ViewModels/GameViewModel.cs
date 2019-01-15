using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public String DayOfWeek { get; set; }
        public String DayOfMonth { get; set; }
        public String TimeOfDay { get; set; }
        public string Venue { get; set; }
    }
}
