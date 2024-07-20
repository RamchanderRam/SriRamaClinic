using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Mapping;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Patients;
using MedicalCenter.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Services
{
    public class PatientServiceTests
    {
        [Fact]
        public void MethodShouldReturnTrueIfCertainPatientExists()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            //Act

            var patientExist = false;

            IPatientService patientService = new PatientService(db, Mock.Of<IMapper>());

            patientExist = patientService.IsPatientProfileCompleted("dfCvHg12");


            // Assert

            Assert.True(patientExist);
        }

        [Fact]

        public void MethodShouldReturnFalseIfPatientDoesNotExist()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientServices = new PatientService(db, Mock.Of<IMapper>());

            //Act

            var exists = patientServices.IsPatientProfileCompleted("ffff");

            //Assert

            Assert.False(exists);

        }

        [Fact]

        public void MethodShouldReturnPatientViewModel()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            var mapper = MockMapper.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientService = new PatientService(db, mapper);

            //Act

            var retrievedPatient = patientService.ChangePatientInfo("dfCvHg13");

            //Assert

            Assert.Equal("Samara 2", retrievedPatient.Address);
            Assert.Equal("Bulgaria", retrievedPatient.Country);
            Assert.Equal("Stara Zagora", retrievedPatient.Town);
        }

        [Fact]

        public async Task MethodShouldUpdatePatientInfo()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            var mapper = MockMapper.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientService = new PatientService(db, mapper);

            //Act
            await patientService.ChangePatient(FakeChangePatient(), "dfCvHg13");

            var changedPatient = patientService.GetPatientById("ffghjkk22");

            //Assert

            Assert.Equal("Thorhavn 14", changedPatient.Address);
            Assert.Equal("Norway", changedPatient.Country);
            Assert.Equal("Oslo", changedPatient.Town);
        }

        [Fact]

        public void MethodShouldReturnTrueIfPatientExistWhenCertainEGNIsPassed()
        {
            //Arrange

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var patientService = new PatientService(db,mapper);

            // Act

            var exists = patientService.IsPatientWithCertainEGNExist("9211067524");

            //Assert

            Assert.True(exists);

        }

        [Fact]
        public void MethodShouldReturnFalseIfThereIsNotPatientWithPassedEGN()
        {
            //Arrange

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var patientService = new PatientService(db, mapper);

            // Act

            var exists = patientService.IsPatientWithCertainEGNExist("9211064444");

            //Assert

            Assert.False(exists);

        }

        [Fact]
        public async Task CheckWhenAddMethodIsCalled()
        {
            //Arrange

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var patientService = new PatientService(db, mapper);

            //Act

            await patientService.AddPatient(FakeAddPatientModel(),"ooXvCb");

            var exist = patientService.IsPatientWithCertainEGNExist("8901238741");

            var patientCountInDatabase = db.Patients.Count();

            var targetPatient = patientService.FindPatientByEGN("8901238741");

            // Assert

            Assert.Equal(3,patientCountInDatabase);
            Assert.Equal("Iceland",targetPatient.Country);
            Assert.Equal("Selfoss",targetPatient.Town);
            Assert.Equal("Reignir 6",targetPatient.Address);
            Assert.Equal("8901238741", targetPatient.EGN);
            Assert.True(exist);
        }

        [Fact]
        public async Task MethodAddShouldThrowsErrorWhenTryToAddPatientWithAlreadyExistingEGN()
        {
            //Arrange

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var patientService = new PatientService(db, mapper);

            //Act

            //await patientService.AddPatient(FakeAddPatientModelExistingEGN(),"xCvNmX");

            //Assert

            await Assert.ThrowsAsync<InvalidOperationException>(async () => 
            {
                await patientService.AddPatient(FakeAddPatientModelExistingEGN(), "xCvNmX");
            });
        }

        [Fact]
        public void MethodShouldReturnsAllPatients()
        {
            //Arrange

            var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatientsWithUserProfile());
            db.SaveChanges();

            var mapper = MockMapper.Instance;

            var patientService = new PatientService(db, mapper);

            //Act
            var allPatients = patientService.GetAllPatients();

            var actualCountOfPatients = allPatients.Count();

            //Assert

            Assert.Equal(2, actualCountOfPatients);
        }

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

        private ICollection<Patient> FakePatientsWithUserProfile()
        {
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Mitko",
                LastName = "Kazakov"
            };

            ApplicationUser user1 = new ApplicationUser
            {
                FirstName = "Nasko",
                LastName = "Mitkov"
            };

            return new List<Patient>
            {
                new Patient
                {
                    Id = "ffghjkk",
                    Address = "Samara 3",
                    UserId = "dfCvHg12",
                    User = user,
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
                    User = user1,
                    Country = "Bulgaria",
                    Town = "Stara Zagora",
                    EGN = "9210067525",
                    DateOfBirth = new DateTime(1992,10,6),
                    IsDeleted = false
                }
            };
        }

        private ChangePatientProfileViewModel FakeChangePatient()
        {
            return new ChangePatientProfileViewModel
            {
                Address = "Thorhavn 14",
                Town = "Oslo",
                Country = "Norway"
            };
        }

        private AddPatientFormModel FakeAddPatientModel()
        {
            return new AddPatientFormModel
            {
                Country = "Iceland",
                Town = "Selfoss",
                Address = "Reignir 6",
                EGN = "8901238741",
                DateOfBirth = new DateTime(1989,1,23)
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
    }
}
