using FluentAssertions;
using MedicalCenter.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());

        [Fact]
        public void IndexActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());
    }
}
