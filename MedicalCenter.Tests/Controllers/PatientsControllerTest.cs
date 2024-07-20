using MedicalCenter.Controllers;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Patients;
using MedicalCenter.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class PatientsControllerTest
    {
        [Fact]
        public void AddActionShouldReturnCorrectView()
        {
            // Assert

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var userManager = MockUserManager.Instance;

            var patientsService = new PatientService(db, mapper);

            var patientController = new PatientsController(patientsService, userManager);

            // Act

            var result = patientController.Add(FakeAddPatientModelExistingEGN());

            //Assert

            Assert.NotNull(result);

            var model = Assert.IsType<Task<IActionResult>>(result);

            
        }

        [Fact]
        public void AddActionShouldReturnCurrentViewModelIfModelIsNotValid()
        {
            // Assert

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var userManager = MockUserManager.Instance;

            var patientsService = new PatientService(db, mapper);

            var patientController = new PatientsController(patientsService, userManager);

            // Act

            var result = patientController.Add(FakeAddPatientModelWrongEGN());

            //Assert

            Assert.NotNull(result);

            var viewResult = Assert.IsType<Task<IActionResult>>(result);

        }

        [Fact]

        public void ActionViewProfileShouldReturnCorrectView() 
        {
            // Assert

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var userManager = MockUserManager.Instance;

            var patientsService = new PatientService(db, mapper);

            var patientController = new PatientsController(patientsService, userManager);

            //Act

            var result = patientController.ViewProfile();

            //Assert

            Assert.NotNull(result);

            Assert.IsType<ViewResult>(result);
        }



        [Fact]
        public void GetAddPatientShouldReturnViewOnlyForPatients()
            => MyController<PatientsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        [Fact]
        public void GetAddPatientShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Patients/Add")
               .To<PatientsController>(c => c.Add());


        [Fact]
        public void PostAddPatientShouldBeMapped()
            => MyRouting
                .Configuration().ShouldMap(request => request
                                       .WithPath("/Patients/Add")
                                       .WithMethod(HttpMethod.Post))
                .To<PatientsController>(c => c.Add(With.Any<AddPatientFormModel>()));


        [Fact]
        public void GetViewRecordShouldReturnViewOnlyForSignedInUsers()
            => MyController<PatientsController>
                .Instance()
                .Calling(c => c.ViewRecord("asddd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        [Fact]
        public void GetViewRecordShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Patients/ViewRecord/asssd")
               .To<PatientsController>(c => c.ViewRecord("asssd"));


        [Theory]
        [InlineData("Bulgaria", "PLovdiv", "Marasha", "9211067524", "11/06/1992")]
        public void PostAddPatientShouldBeOnlyForPatientsAndRedirectWithValidModel(string country, string town, string address, string egn, string date)
            => MyController<PatientsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new AddPatientFormModel
                {
                    Country = country,
                    Town = town,
                    Address = address,
                    EGN = egn,
                    DateOfBirth = DateTime.Parse(date),

                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Patient>(patients => patients
                        .Any(p =>
                            p.EGN == egn)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PatientsController>(c => c.ViewProfile()));

        private ICollection<Patient> FakePatients()
        {
            return new List<Patient>
            {
                new Patient
                {
                    Id = "ffghjkk",
                    Address = "Samara 3",
                    UserId = "dfCvHg12",
                    Country = "Bulgaria",
                    Town = "Stara Zagora",
                    EGN = "9211067524",
                    DateOfBirth = new DateTime(1992,11,6),
                    IsDeleted = false
                },
                new Patient
                {
                    Id = "ffghjkk22",
                    Address = "Samara 2",
                    UserId = "dfCvHg13",
                    Country = "Bulgaria",
                    Town = "Stara Zagora",
                    EGN = "9210067525",
                    DateOfBirth = new DateTime(1992,10,6),
                    IsDeleted = false
                }
            };
        }

        private AddPatientFormModel FakeAddPatientModelExistingEGN()
        {
            return new AddPatientFormModel
            {
                Country = "Russia",
                Town = "Moscow",
                Address = "Pripiyat 13",
                EGN = "9211067524",
                DateOfBirth = new DateTime(1992, 11, 6)
            };
        }

        private AddPatientFormModel FakeAddPatientModelWrongEGN()
        {
            return new AddPatientFormModel
            {
                Country = "Russia",
                Town = "Moscow",
                Address = "Pripiyat 13",
                EGN = "921106752",
                DateOfBirth = new DateTime(1992, 11, 6)
            };
        }

    }
}
