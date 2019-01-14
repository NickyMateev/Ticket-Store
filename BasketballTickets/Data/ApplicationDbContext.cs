using System;
using System.Collections.Generic;
using System.Text;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketballTickets.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Arena> Arenas { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
