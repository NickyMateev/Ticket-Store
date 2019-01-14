using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey("GameTypeId")]
        public virtual GameType GameType { get; set; }

        [ForeignKey("LeagueId")]
        public virtual League League{ get; set; }
    }
}
