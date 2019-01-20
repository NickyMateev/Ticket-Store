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
    public class GamesController : BaseController
    {
        public GamesController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Games
        public async Task<IActionResult> Index(int? teamId, int? gameTypeId, bool hideAwayGames = false)
        {
            ViewData["GamesTitle"] = "Games";
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name");

            IQueryable<Game> games = _context.Games.Include(g => g.AwayTeam).Include(g => g.GameType).Include(g => g.HomeTeam);
            if (teamId != null)
            {
                String teamName = _context.Teams.Where(t => t.Id == teamId).First().Name;
                ViewData["GamesTitle"] = teamName + " games";
                ViewData["TeamId"] = teamId;

                games = games.Where(g => (g.HomeTeamId == teamId) || (g.AwayTeamId == teamId));

                if (hideAwayGames)
                {
                    games = games.Where(g => g.HomeTeamId == teamId);
                }
            }

            if (gameTypeId != null)
            {
                games = games.Where(g => g.GameTypeId == gameTypeId);
            }

            games = games.OrderBy(g => g.Date);

            return View(buildGameViewModels(games, gameTypeId));
        }

        private ICollection<CategorizedGamesViewModel> buildGameViewModels(IQueryable<Game> games, int? gameTypeId)
        {
            var gameTypes = _context.GameTypes.OrderBy(g => g.Order).ToList();
            List<CategorizedGamesViewModel> categorizedGames = new List<CategorizedGamesViewModel>();
            foreach(var gameType in gameTypes)
            {
               if (gameTypeId != null)
                {
                    if (gameType.Id == gameTypeId)
                    {
                        categorizedGames.Add(
                            new CategorizedGamesViewModel
                            {
                                GamesType = gameType.Name,
                                Games = games.Where(g => g.GameTypeId == gameType.Id).Select(g => new GameViewModel
                                    {
                                        Game = g,
                                        DayOfWeek = DateService.GetDayOfWeek(g.Date),
                                        DayOfMonth = DateService.GetDayOfMonth(g.Date),
                                        TimeOfDay = DateService.GetTimeOfDay(g.Date),
                                        Venue = _context.Arenas.Where(a => a.Id == g.HomeTeam.ArenaId).First().Name
                                    }).ToList()
                            });
                        break;
                    }
                } else
                {
                    categorizedGames.Add(
                        new CategorizedGamesViewModel
                        {
                            GamesType = gameType.Name,
                            Games = games.Where(g => g.GameTypeId == gameType.Id).Select(g => new GameViewModel
                                {
                                    Game = g,
                                    DayOfWeek = DateService.GetDayOfWeek(g.Date),
                                    DayOfMonth = DateService.GetDayOfMonth(g.Date),
                                    TimeOfDay = DateService.GetTimeOfDay(g.Date),
                                    Venue = _context.Arenas.Where(a => a.Id == g.HomeTeam.ArenaId).First().Name
                                }).ToList()
                        });
                }
            }

            return categorizedGames;
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Date,HomeTeamId,AwayTeamId,GameTypeId,LeagueId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.AwayTeamId);
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name", game.GameTypeId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", game.HomeTeamId);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Admin")]
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
