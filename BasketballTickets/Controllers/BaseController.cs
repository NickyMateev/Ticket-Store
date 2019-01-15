using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TempData["showLeagues"] = true;
            ViewData["Leagues"] = new List<League>(_context.Leagues.AsNoTracking().ToList());
        }
    }
}
