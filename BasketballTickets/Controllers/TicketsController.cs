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

namespace BasketballTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketsController : BaseController
    {
        public TicketsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Tickets
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

        // GET: Tickets/Details/5
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
