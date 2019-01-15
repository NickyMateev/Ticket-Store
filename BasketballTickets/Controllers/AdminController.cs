using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketballTickets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Reset()
        {
            _context.Tickets.RemoveRange(_context.Tickets);
            _context.Games.RemoveRange(_context.Games);
            _context.GameTypes.RemoveRange(_context.GameTypes);
            _context.Teams.RemoveRange(_context.Teams);
            _context.Leagues.RemoveRange(_context.Leagues);
            _context.Arenas.RemoveRange(_context.Arenas);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}