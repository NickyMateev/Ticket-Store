using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballTickets.Data;
using BasketballTickets.Models;

namespace BasketballTickets.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Games.Include(g => g.AwayTeam).Include(g => g.GameType).Include(g => g.HomeTeam).Include(g => g.League);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.AwayTeam)
                .Include(g => g.GameType)
                .Include(g => g.HomeTeam)
                .Include(g => g.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Id");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Id");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,HomeTeamId,AwayTeamId,GameTypeId,LeagueId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Id", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Id", game.LeagueId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Id", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Id", game.LeagueId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,HomeTeamId,AwayTeamId,GameTypeId,LeagueId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Id", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id", game.HomeTeamId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Id", game.LeagueId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.AwayTeam)
                .Include(g => g.GameType)
                .Include(g => g.HomeTeam)
                .Include(g => g.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
