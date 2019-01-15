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
        public Team HomeTeam { get; set; }

        public int AwayTeamId{ get; set; }
        public Team AwayTeam { get; set; }

        public int GameTypeId { get; set; }
        public GameType GameType { get; set; }

        public int LeagueId { get; set; }
        public League League{ get; set; }
    }
}
