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

namespace BasketballTickets.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index(int? leagueId)
        {
            var teams = _context.Teams;
            if (leagueId != null)
            {
                return View(await teams.Where(t => t.LeagueId == leagueId).ToListAsync());
            }

            return View(await teams.Where(t => t.League.Name.Equals("NBA")).ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
