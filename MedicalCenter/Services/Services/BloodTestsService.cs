using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.BloodTests;
using MedicalCenter.Services.ViewModels.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class BloodTestsService : IBloodTestsService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public BloodTestsService(IMapper mapper, ApplicationDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        public IEnumerable<ListAllParametersViewModel> AllParametersForSingleTest(string testId)
        {
            var allParams = this.db.BloodTestsPatients.Where(bt => bt.BloodTestId == testId).ProjectTo<ListAllParametersViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allParams;
        }

        public async Task FillBloodTest(double[] parameters, string testId)
        {
            BloodTest test = this.db.BloodTests.FirstOrDefault(t => t.Id == testId);

            test.IsCompleted = true;

            var paramsToEnter = this.db.BloodTestsPatients.Where(t => t.BloodTestId == testId);

            int count = 0;

            foreach (var p in paramsToEnter)
            {
                p.Value = parameters[count];
                count++;
            }

            await this.db.SaveChangesAsync();
        }

        public IEnumerable<ListAllParametersViewModel> ListAllParameters()
        {
            var allParameters = this.db.Parameters.ProjectTo<ListAllParametersViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allParameters;
        }

        public IEnumerable<AllBloodTestsViewModel> ListAllUnfinishedTests(string patientId)
        {
            var allTests = this.db.BloodTests.Where(t => t.PatientId == patientId && t.IsCompleted == false).ProjectTo<AllBloodTestsViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allTests;
        }

        public IEnumerable<AllBloodTestsViewModel> ListAllFinishedTests(string patientId)
        {
            var allTests = this.db.BloodTests.Where(t => t.PatientId == patientId && t.IsCompleted == true).ProjectTo<AllBloodTestsViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allTests;
        }

        public async Task SendBloodTest(List<string> checkedParams, string doctorId, string patientId)
        {
            Doctor doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == doctorId);

            Patient patient = this.db.Patients.FirstOrDefault(p => p.Id == patientId);

            BloodTest bloodTest = new BloodTest()
            {
                CreatedOn = DateTime.UtcNow,
                Doctor = doctor,
                Patient = patient
            };

            foreach (var param in checkedParams)
            {
                Parameter parameter = this.db.Parameters.AsEnumerable().FirstOrDefault(p => p.Name == param);

                BloodTestsPatients testsPatients = new BloodTestsPatients()
                {
                    BloodTest = bloodTest,
                    PatientId = patient.Id,
                    ParameterId = parameter.Id
                };

                bloodTest.BloodTestsPatients.Add(testsPatients);

            }

            await this.db.BloodTests.AddAsync(bloodTest);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<ResultsViewModel> GetSingleResult(string testId)
        {
            var results = this.db.BloodTestsPatients.Where(tp => tp.BloodTestId == testId).ProjectTo<ResultsViewModel>(this.mapper.ConfigurationProvider).ToList();

            return results;
        }
    }
}
