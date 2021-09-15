using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBook.Data
{
    public class Seeder
    {
        public static async Task CreateRole(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ContactBookDBContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                AppUser user = new AppUser
                {
                    FirstName = "Mho",
                    LastName = "'et",
                    Email = "mho'et@gmail.com",
                    UserName = "Mhoet",
                    PhoneNumber = "09087488822"
                };
                await userManager.CreateAsync(user, "Abcd12@");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
