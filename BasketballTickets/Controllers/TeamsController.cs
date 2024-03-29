﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Authorization;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BasketballTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamsController : BaseController 
    {
        private readonly IUploadableFile _upload;
        private readonly String teamsFolder;

        public TeamsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUploadableFile upload) : base(context, userManager)
        {
            _upload = upload;
            teamsFolder = "teams";
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teams.Include(t => t.Arena).Include(t => t.League);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Arena)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "Name");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,LogoPath,ArenaId,LeagueId")] Team team, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                team.LogoPath = "~/uploads/" + teamsFolder + "/" + file.FileName.Trim();
                _upload.UploadFile(file, teamsFolder);
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "Name", team.ArenaId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "Name", team.ArenaId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,LogoPath,ArenaId,LeagueId")] Team team, IFormFile file)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        team.LogoPath = "~/uploads/" + teamsFolder + "/" + file.FileName.Trim();
                        _upload.UploadFile(file, teamsFolder);
                    } else
                    {
                        team.LogoPath = _context.Teams.AsNoTracking().Where(t => t.Id == team.Id).First().LogoPath;
                    }
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            ViewData["ArenaId"] = new SelectList(_context.Arenas, "Id", "Name", team.ArenaId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Arena)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
