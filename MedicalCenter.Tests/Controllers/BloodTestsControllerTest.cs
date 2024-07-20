using MedicalCenter.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class BloodTestsControllerTest
    {
        [Fact]
        public void GetViewBloodTestsShouldReturnViewOnlyForDocs()
            => MyController<BloodTestsController>
                .Instance()
                .Calling(c => c.ViewBloodTests("ddd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetViewBloodTestsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/BloodTests/ViewBloodTests/dd")
               .To<BloodTestsController>(c => c.ViewBloodTests("dd"));


        [Fact]
        public void GetSendBloodTestsShouldReturnViewOnlyForDocs()
            => MyController<BloodTestsController>
                .Instance()
                .Calling(c => c.SendBloodTests("ddd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetSendBloodTestsShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/BloodTests/SendBloodTests/dd")
              .To<BloodTestsController>(c => c.SendBloodTests("dd"));

        [Fact]
        public void GetAllBloodTestsShouldReturnViewOnlyForDocs()
           => MyController<BloodTestsController>
               .Instance()
               .Calling(c => c.AllBloodTests("ddd"))
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetAllBloodTestsShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/BloodTests/AllBloodTests/dd")
              .To<BloodTestsController>(c => c.AllBloodTests("dd"));

        [Fact]
        public void GetAllParameterssShouldReturnViewOnlyForDocs()
           => MyController<BloodTestsController>
               .Instance()
               .Calling(c => c.AllParameters("ddd"))
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetAllParametersShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/BloodTests/AllParameters/dd")
              .To<BloodTestsController>(c => c.AllParameters("dd"));

        [Fact]
        public void GetSeeResultsShouldReturnViewOnlyForDocs()
           => MyController<BloodTestsController>
               .Instance()
               .Calling(c => c.SeeResults("ddd"))
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetSeeResultsShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/BloodTests/SeeResults/dd")
              .To<BloodTestsController>(c => c.SeeResults("dd"));
    }
}
