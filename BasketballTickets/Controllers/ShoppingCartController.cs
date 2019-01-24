using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasketballTickets.Controllers
{
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]int ticketId)
        {
            if (ticketId == null)
            {
                return NotFound();
            }

            Ticket ticket = _context.Tickets.Where(t => t.Id == ticketId).SingleOrDefault();
            if (ticket == null || ticket.ShoppingCartId != null)
            {
                return RedirectToAction("Index", "Games");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            ShoppingCart shoppingCart = null;
            if (!_context.ShoppingCarts.Any(sc => sc.UserId == user.Id))
            {
                shoppingCart = new ShoppingCart { Tickets = new List<Ticket>() { ticket }, User = user };
                _context.ShoppingCarts.Add(shoppingCart);
            } else
            {
                shoppingCart = _context.ShoppingCarts.Include(sc => sc.Tickets).Where(sc => sc.UserId == user.Id).SingleOrDefault();
                if (!shoppingCart.Tickets.Any(t => t.Id == ticketId))
                {
                    shoppingCart.Tickets.Add(ticket);
                    _context.ShoppingCarts.Update(shoppingCart);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Games");
        }

        public async Task<IActionResult> Remove([FromBody]int ticketId)
        {
            if (ticketId == null)
            {
                return NotFound();
            }

            Ticket ticket = _context.Tickets.Include(t => t.ShoppingCart).Where(t => t.Id == ticketId).SingleOrDefault();
            if (ticket == null || ticket.ShoppingCartId == null)
            {
                return RedirectToAction("Index", "Games");
            }

            var shoppingCart = ticket.ShoppingCart;
            shoppingCart.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Games");
        }
    }
}