using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballTickets.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Testy!123";

            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, adminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
