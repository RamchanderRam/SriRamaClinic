using MedicalCenter.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalCenter.Tests.Mocks
{
    public static class MockUserManager
    {
        public static Mock<UserManager<TUser>> UserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        private static List<ApplicationUser> users ()
        {

            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                Id = "asXX",
                FirstName = "Mitko",
                LastName = "Kazakov",
                Email = "a@abv.bg"
                },
            new ApplicationUser
                {
                Id = "asXXZ",
                FirstName = "Nasko",
                LastName = "Vikoc",
                Email = "b@abv.bg"
                }
            };
        }

        public static UserManager<ApplicationUser> Instance
        {
            get 
            {
                var userManager = UserManager<ApplicationUser>(users()).Object;

                return userManager;
            }
        }
    }
}
