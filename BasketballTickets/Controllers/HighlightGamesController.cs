using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Identity;
using FileUploadControl;
using Microsoft.AspNetCore.Http;

namespace BasketballTickets.Controllers
{
    public class HighlightGamesController : BaseController
    {
        private readonly IUploadableFile _upload;
        private readonly String imageFolder;

        public HighlightGamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUploadableFile upload) : base(context, userManager)
        {
            _upload = upload;
            imageFolder = "highlights";
        }

        // GET: HighlightGames
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HighlightGames.Include(h => h.Game);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HighlightGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlightGame = await _context.HighlightGames
                .Include(h => h.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highlightGame == null)
            {
                return NotFound();
            }

            return View(highlightGame);
        }

        // GET: HighlightGames/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            return View();
        }

        // POST: HighlightGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ImagePath,IsBanner,GameId")] HighlightGame highlightGame, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                highlightGame.ImagePath = "~/uploads/" + imageFolder + "/" + file.FileName.Trim();
                _upload.UploadFile(file, imageFolder);
                _context.Add(highlightGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", highlightGame.GameId);
            return View(highlightGame);
        }

        // GET: HighlightGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlightGame = await _context.HighlightGames.FindAsync(id);
            if (highlightGame == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", highlightGame.GameId);
            return View(highlightGame);
        }

        // POST: HighlightGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImagePath,IsBanner,GameId")] HighlightGame highlightGame, IFormFile file)
        {
            if (id != highlightGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        highlightGame.ImagePath = "~/uploads/" + imageFolder + "/" + file.FileName.Trim();
                        _upload.UploadFile(file, imageFolder);
                    }
                    else
                    {
                        highlightGame.ImagePath = _context.Leagues.AsNoTracking().Where(l => l.Id == highlightGame.Id).First().LogoPath;
                    }

                    _context.Update(highlightGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighlightGameExists(highlightGame.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id", highlightGame.GameId);
            return View(highlightGame);
        }

        // GET: HighlightGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlightGame = await _context.HighlightGames
                .Include(h => h.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highlightGame == null)
            {
                return NotFound();
            }

            return View(highlightGame);
        }

        // POST: HighlightGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var highlightGame = await _context.HighlightGames.FindAsync(id);
            _context.HighlightGames.Remove(highlightGame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HighlightGameExists(int id)
        {
            return _context.HighlightGames.Any(e => e.Id == id);
        }
    }
}
