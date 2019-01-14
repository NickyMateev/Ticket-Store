using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketballTickets.Data;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTickets.Controllers
{
    public class GamesController : Controller
    {
        ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Games
        public ActionResult Index(int? teamId)
        {
            List<Game> games;
            if (teamId != null)
            {
                games = _context.Games.Where(g => g.HomeTeam.Id == teamId).ToList();
            } else
            {
                games = _context.Games.ToList();
            }

            return View(games);
        }

        // GET: Games/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            var leagues = _context.Leagues.ToList();
            var gameTypes = _context.GameTypes.ToList();
            var arenas = _context.Arenas.ToList();

            ViewData["leagues"] = leagues;
            ViewData["gameTypes"] = gameTypes;
            ViewData["arenas"] = arenas;

            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Games/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}