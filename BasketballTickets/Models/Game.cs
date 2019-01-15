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

        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        public int GameTypeId { get; set; }
        public virtual GameType GameType { get; set; }

        public int LeagueId { get; set; }
        public virtual League League{ get; set; }
    }
}
