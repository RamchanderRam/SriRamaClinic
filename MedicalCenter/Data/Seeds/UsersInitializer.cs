using MedicalCenter.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicalCenter.Data.Seeds
{
    public static class UsersInitializer
    {
        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync
                           ("mitko@tpp2.com") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "mitko@tpp2.com";
                user.Email = "mitko@tpp2.com";
                user.FirstName = "Dimitar";
                user.LastName = "Kazakov";

                var result = await userManager.CreateAsync
                (user, "Jameson92@");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }
        }
    }
}
