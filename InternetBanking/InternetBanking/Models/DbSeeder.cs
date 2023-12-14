using InternetBanking.Areas.Identity.Data;
using InternetBanking.Constants;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Models
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<InternetBankingUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Employee.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //create admin
            var user = new InternetBankingUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "admin",
                EmailConfirmed = true

            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if(userInDb == null)
            {
                await userManager.CreateAsync(user, "admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}
