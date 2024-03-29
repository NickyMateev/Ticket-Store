﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasketballTickets.Models;
using BasketballTickets.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace BasketballTickets.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        }

        public async Task<IActionResult> Index(int? leagueId)
        {
            IQueryable<Team> teams = _context.Teams;
            if (leagueId != null)
            {
                teams = teams.Where(t => t.LeagueId == leagueId); 
            }

            var teamsFilter = teams.Select(t => new
            {
                Id = t.Id,
                Name = t.City + " " + t.Name
            });

            ViewData["TeamId"] = new SelectList(teamsFilter, "Id", "Name");
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name");
            ViewData["BannerHighlightGame"] = _context.HighlightGames.Include(hg => hg.Game).Where(hg => hg.IsBanner == true).OrderByDescending(hg => hg.Game.Date).FirstOrDefault();
            ViewData["HighlightGames"] = _context.HighlightGames.Include(hg => hg.Game).Where(hg => hg.IsBanner == false).OrderByDescending(hg => hg.Game.Date).Take(4).ToList();
            return View(await teams.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
