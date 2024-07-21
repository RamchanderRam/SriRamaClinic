using MedicalCenter.Data.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Data.Seeds
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> SeedDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedUsers(services);
            await SeedDocs(services);

            return app;
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Patient", "Doctor", "Laboratory Assistant" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUsers(IServiceProvider serviceProvider)
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

        private static async Task SeedDocs(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //if (await userManager.FindByNameAsync
            //               ("mitko1@abv.bg") == null)
            //{
            //    ApplicationUser user = new ApplicationUser();
            //    user.UserName = "mitko1@abv.bg";
            //    user.Email = "mitko1@abv.bg";
            //    user.FirstName = "Russel";
            //    user.LastName = "Dowson";

            //    var result = await userManager.CreateAsync
            //    (user, "Jameson92@");

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user,
            //                            "Doctor").Wait();

            //        Doctor doctor = new Doctor()
            //        {
            //            Specialty = "Anesthesiologist",
            //            Biography = "Russel Dowson, M.D., is an anesthesiologist and pain medicine physician. He is currently a Mayo Clinic Scholar. In addition to his clinical activities, Dr. Dowson is active in clinical research in areas of spinal cord stimulation and regenerative pain medicine techniques. Dr. Dowson's clinical interests involve providing high-quality multimodal pain control for patients with acute and chronic pain. He delivers interventional and pharmacologic treatments for adults with a variety of pain complaints.",
            //            UserId = user.Id
            //        };

            //        await db.Doctors.AddAsync(doctor);
            //        await db.SaveChangesAsync();
            //    }
            //}


            if (await userManager.FindByNameAsync
                           ("mitko2@abv.bg") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "mitko2@abv.bg";
                user.Email = "mitko2@abv.bg";
                user.FirstName = "Sasha";
                user.LastName = "Gray";

                var result = await userManager.CreateAsync
                (user, "Jameson92@");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Doctor").Wait();

                    Doctor doctor = new Doctor()
                    {
                        Specialty = "Ophthalmologist",
                        Biography = "Sasha Gray, M.D., is an ophthalmologist with subspecialty training in ocular oncology. Dr. Gray is also involved in research and education. She provides mentorship to medical students, residents, and fellows. She engages in clinical and translational research and authors expert content in scientific journals to advance the field of ocular oncology.",
                        UserId = user.Id
                    };

                    await db.Doctors.AddAsync(doctor);
                    await db.SaveChangesAsync();
                }
            }

            if (await userManager.FindByNameAsync
                           ("mitko3@abv.bg") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "mitko3@abv.bg";
                user.Email = "mitko3@abv.bg";
                user.FirstName = "Thorsten";
                user.LastName = "Gunnarson";

                var result = await userManager.CreateAsync
                (user, "Jameson92@");

                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user,
                //                        "Doctor").Wait();

                //    Doctor doctor = new Doctor()
                //    {
                //        Specialty = "Surgeon",
                //        Biography = "Thorsten Gunnarson, M.D., Ph.D., is a cardiothoracic surgeon with specialty interest in thoracic oncology, general thoracic and foregut surgery, lung transplantation/lung failure, and ECMO. Dr. Gunnarson is active in research and education, providing mentorship to residents, fellows and junior faculty. He authors expert content and publishes in high-impact scientific journals.",
                //        UserId = user.Id
                //    };

                //    await db.Doctors.AddAsync(doctor);
                //    await db.SaveChangesAsync();
                //}
            }

            //if (await userManager.FindByNameAsync
            //               ("mitko4@abv.bg") == null)
            //{
            //    ApplicationUser user = new ApplicationUser();
            //    user.UserName = "mitko4@abv.bg";
            //    user.Email = "mitko4@abv.bg";
            //    user.FirstName = "Lui";
            //    user.LastName = "Jerson";

            //    var result = await userManager.CreateAsync
            //    (user, "Jameson92@");

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user,
            //                            "Doctor").Wait();

            //        Doctor doctor = new Doctor()
            //        {
            //            Specialty = "Endocrinology",
            //            Biography = @"Eugene Sobngwi is Professor of Endocrinology and Metabolism at the University of Yaoundé and Consultant Endocrinologist at Yaoundé Central Hospital. He is head of the Laboratory of Molecular Medicine and Metabolism at the Biotechnology Centre of the University of Yaoundé. His main research areas are epidemiology and pathophysiology of diabetes in Africa, clinical trials, and diabetes in pregnancy. He is author of over 150 peer-reviewed publications and 10 book chapters including chapters in the French Textbook of Diabetes and the Oxford Textbook of Endocrinology and Diabetes.",
            //            UserId = user.Id
            //        };

            //        await db.Doctors.AddAsync(doctor);
            //        await db.SaveChangesAsync();
            //    }
            //}

            if (await userManager.FindByNameAsync
                           ("mitko5@abv.bg") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "mitko5@abv.bg";
                user.Email = "mitko5@abv.bg";
                user.FirstName = "Natasha";
                user.LastName = "Emerald";

                var result = await userManager.CreateAsync
                (user, "Jameson92@");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Laboratory Assistant").Wait();

                    Doctor doctor = new Doctor()
                    {
                        Specialty = "Laboratory Assistant",
                        Biography = @"Natasha Emerald is Laboratory Assistant of Endocrinology and Metabolism at the University of Yaoundé and Consultant Endocrinologist at Yaoundé Central Hospital. He is head of the Laboratory of Molecular Medicine and Metabolism at the Biotechnology Centre of the University of Yaoundé. His main research areas are epidemiology and pathophysiology of diabetes in Africa, clinical trials, and diabetes in pregnancy. He is author of over 150 peer-reviewed publications and 10 book chapters including chapters in the French Textbook of Diabetes and the Oxford Textbook of Endocrinology and Diabetes.",
                        UserId = user.Id
                    };

                    await db.Doctors.AddAsync(doctor);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
