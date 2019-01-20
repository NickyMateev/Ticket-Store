using System;
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

namespace BasketballTickets.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index(int? leagueId)
        {
            IQueryable<Team> teams = _context.Teams;
            if (leagueId != null)
            {
                teams = teams.Where(t => t.LeagueId == leagueId); 
            } 

            ViewData["TeamId"] = new SelectList(teams, "Id", "Name");
            ViewData["GameTypeId"] = new SelectList(_context.GameTypes, "Id", "Name");
            return View(await teams.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
