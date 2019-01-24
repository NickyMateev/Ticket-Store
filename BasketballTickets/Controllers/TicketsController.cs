using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Authorization;
using BasketballTickets.Services;
using BasketballTickets.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BasketballTickets.Controllers
{
    public class TicketsController : BaseController
    {
        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        }

        // GET: Tickets
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(t => t.Game).Include(t => t.Game.HomeTeam).Include(t => t.Game.AwayTeam);
            var formattedTickets = applicationDbContext.Select(t => new TicketsViewModel
            {
                Id = t.Id,
                SeatNo = t.SeatNo,
                Price = t.Price,
                Game = DateService.GetFormattedGameDate(t.Game.AwayTeam.Name, t.Game.HomeTeam.Name, t.Game.Date)
            });
            return View(formattedTickets);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Generate(int? gameId)
        {
            if (gameId == null)
            {
                return NotFound();
            }

            Game game = _context.Games.Include(g => g.Tickets).Include(g => g.HomeTeam).Include(g => g.HomeTeam.Arena).Where(g => g.Id == gameId).First();
            int maxTickets = game.HomeTeam.Arena.Capacity;

            ICollection<Ticket> ticketsToAdd = TicketService.GenerateTickets(game, maxTickets);
            foreach (Ticket ticket in ticketsToAdd)
            {
                game.Tickets.Add(ticket);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Games");
        }

        public async Task<IActionResult> Book(int? gameId)
        {
            if (gameId == null)
            {
                return NotFound();
            }

            var game = _context.Games.Where(g => g.Id == gameId)
                .Include(g => g.GameType)
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .Include(g => g.Tickets)
                .Include(g => g.Tickets)
                .Include(g => g.HomeTeam.Arena).First();
            ViewData["GameDate"] = DateService.GetDayOfWeek(game.Date) + ", " + DateService.GetDayOfMonth(game.Date) + " - " + DateService.GetTimeOfDay(game.Date);

            var viewModel = new TicketsBookingViewModel()
            {
                Game = game,
                AvailableTickets = new List<Ticket>(),
                BookedTickets = new List<Ticket>()
            };

            var user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (Ticket ticket in game.Tickets)
            {
                if (ticket.ShoppingCartId == null)
                {
                    viewModel.AvailableTickets.Add(ticket);
                } else if (ticket.ShoppingCart.UserId == user.Id) {
                    viewModel.BookedTickets.Add(ticket);
                }
            }

            return View(viewModel);
        }


        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Game)
                .Include(t => t.Game.HomeTeam)
                .Include(t => t.Game.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            var gameTicket = DateService.GetFormattedGameDate(ticket.Game.AwayTeam.Name, ticket.Game.HomeTeam.Name, ticket.Game.Date);
            ViewData["GameTicket"] = gameTicket;
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var  games = _context.Games.OrderBy(g => g.Date).Include(g => g.HomeTeam).Include(g => g.AwayTeam).ToList();
            List<object> formattedGames = new List<object>();
            foreach (var game in games)
            {
                formattedGames.Add(new
                {
                    Id = game.Id,
                    Name = DateService.GetFormattedGameDate(game.AwayTeam.Name, game.HomeTeam.Name, game.Date)
                });
            }
            ViewData["GameId"] = new SelectList(formattedGames, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,SeatNo,Price,GameId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", ticket.GameId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var games = _context.Games.OrderBy(g => g.Date).Include(g => g.HomeTeam).Include(g => g.AwayTeam).ToList();
            List<object> formattedGames = new List<object>();
            foreach (var game in games)
            {
                formattedGames.Add(new
                {
                    Id = game.Id,
                    Name = DateService.GetFormattedGameDate(game.AwayTeam.Name, game.HomeTeam.Name, game.Date)
                });
            }
            ViewData["GameId"] = new SelectList(formattedGames, "Id", "Name", ticket.GameId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeatNo,Price,GameId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", ticket.GameId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
