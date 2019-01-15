using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models.ViewModels
{
    public class TeamViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string LogoPath { get; set; }

        public int ArenaId { get; set; }
        public List<SelectListItem> Arenas { get; set; }
    }
}
