using MedicalCenter.Controllers;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Doctors;
using MedicalCenter.Services.ViewModels.Patients;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class DoctorsControllerTest
    {
        [Fact]
        public void GetAddDoctorsShouldReturnViewOnlyForPatients()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.Add())
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetAddDoctorsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Doctors/Add")
               .To<DoctorsController>(c => c.Add());

        [Fact]
        public void PostAddDoctorsShouldBeMapped()
            => MyRouting
                .Configuration().ShouldMap(request => request
                                       .WithPath("/Doctors/Add")
                                       .WithMethod(HttpMethod.Post))
                .To<DoctorsController>(c => c.Add(With.Any<AddDoctorFormModel>()));

        [Fact]
        public void GetViewProfileShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Doctors/ViewProfile")
              .To<DoctorsController>(c => c.ViewProfile());

        [Fact]
        public void GetViewProfileShouldReturnViewOnlyForDoctors()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.ViewProfile())
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetChangeProfileShouldReturnViewOnlyForDoctors()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.ChangeProfile("dddd"))
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Fact]
        public void GetCHangeProfileShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Doctors/ChangeProfile/dddd")
              .To<DoctorsController>(c => c.ChangeProfile("dddd"));

        [Fact]
        public void GetFindPatientByEGNShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Doctors/FindPatientByEGN")
              .To<DoctorsController>(c => c.FindPatientByEGN());

        [Fact]
        public void GetFindPatientByEGNShouldReturnViewOnlyForDoctors()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.FindPatientByEGN())
               .ShouldHave()
               .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Theory]
        [InlineData("9211067524")]
        public void PostPatientProfileShouldBeOnlyForPatientsAndRedirectWithValidModel(string egn)
            => MyController<DoctorsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.PatientProfile(new FindPatientEGNFormModel
                {
                    EGN = egn

                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .View("FindPatientByEGN");

        [Fact]
        public void GetAllDocsShouldReturnView()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.AllDoctors())
               .ShouldReturn()
               .View();

        [Fact]
        public void GetAllDocsShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Doctors/AllDoctors")
              .To<DoctorsController>(c => c.AllDoctors());

        [Fact]
        public void GetViewDocShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Doctors/ViewDoctor/aaaa")
              .To<DoctorsController>(c => c.ViewDoctor("aaaa"));

        [Fact]
        public void GetViewDochouldReturnView()
           => MyController<DoctorsController>
               .Instance()
               .Calling(c => c.ViewDoctor("aaaa"))
               .ShouldReturn()
               .Redirect();
    }
}

