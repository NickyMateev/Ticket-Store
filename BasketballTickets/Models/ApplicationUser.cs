using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
