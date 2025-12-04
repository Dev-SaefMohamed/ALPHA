using Hero.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Hero.Data.DataSeeder
{
    public class DataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            // ----- Roles -----
            var roles = new[] { "Admin", "Corporate" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            // ----- Admin User -----
            string adminEmail = "admin@hero.com";
            string adminPassword = "Admin123!";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                await _userManager.CreateAsync(adminUser, adminPassword);
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
