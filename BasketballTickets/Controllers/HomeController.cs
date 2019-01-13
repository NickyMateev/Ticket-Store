using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasketballTickets.Models;
using BasketballTickets.Data;

namespace BasketballTickets.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var teams = _context.Teams.ToList();
            return View(teams);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
