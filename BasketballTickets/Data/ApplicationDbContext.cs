using System;
using System.Collections.Generic;
using System.Text;
using BasketballTickets.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                });

            modelBuilder.Entity<Team>().HasMany(t => t.HomeGames)
                .WithOne(g => g.HomeTeam)
                .HasForeignKey(g => g.HomeTeamId);

            modelBuilder.Entity<Team>().HasMany(t => t.AwayGames)
                .WithOne(g => g.AwayTeam)
                .HasForeignKey(g => g.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Arena> Arenas { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
