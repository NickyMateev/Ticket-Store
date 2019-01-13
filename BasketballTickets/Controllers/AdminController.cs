using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketballTickets.Data;
using BasketballTickets.Models;
using BasketballTickets.Models.ViewModels;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTickets.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private IUploadableFile _upload;

        public AdminController(ApplicationDbContext context, IUploadableFile upload)
        {
            this._context = context;
            this._upload = upload;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateTeam()
        {
            if (TempData["showSuccessfulMsgStrip"] == null)
            {
                TempData["showSuccessfulMsgStrip"] = false;
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(IFormFile file, TeamViewModel teamViewModel, Team team)
        {
            team.Name = teamViewModel.Name;
            team.City = teamViewModel.City;
            team.LogoPath = "~/uploads/" + file.FileName.Trim();
            _upload.UploadFile(file);
            _context.Teams.Add(team);
            _context.SaveChanges();

            TempData["showSuccessfulMsgStrip"] = true;
            TempData["Success"] = "Successfully created the " + team.City + " " + team.Name + "!";
            return RedirectToAction("CreateTeam", "Admin");
        }
    }
}