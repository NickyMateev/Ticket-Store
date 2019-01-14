using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class Arena
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
