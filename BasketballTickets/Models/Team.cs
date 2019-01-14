using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string LogoPath { get; set; }

        [ForeignKey("ArenaId")]
        public virtual Arena Arena { get; set; }

        public virtual ICollection<Game> HomeGames { get; set; }
    }
}
