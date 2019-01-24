using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            TempData["showLeagues"] = true;
            ViewData["Leagues"] = new List<League>(_context.Leagues.AsNoTracking().ToList());

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null && _context.ShoppingCarts.Any(sc => sc.UserId == user.Id))
            {
                var shoppingCartId = _context.ShoppingCarts.Where(sc => sc.UserId == user.Id).SingleOrDefault().Id;
                var tickets = _context.Tickets
                    .Include(t => t.Game.HomeTeam)
                    .Include(t => t.Game.AwayTeam)
                    .Where(t => t.ShoppingCartId == shoppingCartId).ToList();
                ViewData["CartQuantity"] = tickets.Count;
                ViewData["ShoppingCart"] = tickets;
            } else
            {
                ViewData["ShoppingCart"] = new List<Ticket>();
            }

            await next();
        }
    }
}
