using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PatientService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool IsPatientProfileCompleted(string userId)
        {
            return this.db.Patients.Any(d => d.UserId == userId);
        }

        public bool IsPatientWithCertainEGNExist(string EGN)
        {
            if (this.db.Patients.Any(p => p.EGN == EGN))
            {
                return true;
            }

            return false;
        }

        public async Task AddPatient(AddPatientFormModel patient,string userId)
        {
            if (this.db.Patients.Any(p => p.EGN == patient.EGN))
            {
                throw new InvalidOperationException("Patient with the same EGN already exist.");
            }

            Patient newPatient = this.mapper.Map<Patient>(patient);

            newPatient.UserId = userId;

            await this.db.Patients.AddAsync(newPatient);
            await this.db.SaveChangesAsync();
        }

        public async Task ChangePatient(ChangePatientProfileViewModel model, string userId)
        {
            Patient patient = this.db.Patients.FirstOrDefault(u => u.UserId == userId);

            patient.Country = model.Country;
            patient.Town = model.Town;
            patient.Address = model.Address;

            await this.db.SaveChangesAsync();
        }

        public ChangePatientProfileViewModel ChangePatientInfo(string userId)
        {
            Patient patient = this.db.Patients.FirstOrDefault(u => u.UserId == userId);

            var patientInfo = this.mapper.Map<ChangePatientProfileViewModel>(patient);

            return patientInfo;
        }

        public PatientProfileViewModel FindPatientByEGN(string egn)
        {
            var patient = this.db.Patients.FirstOrDefault(p => p.EGN == egn);

            PatientProfileViewModel patientProfile = null;

            if (patient != null)
            {
                patientProfile = this.mapper.Map<PatientProfileViewModel>(patient);
            }

            return patientProfile;
        }

        public IEnumerable<PatientProfileViewModel> GetAllPatients()
        {
            var allPatients = this.db.Patients.ProjectTo<PatientProfileViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allPatients;
        }

        public PatientTestsAndDiagnosesViewModel GetPatientMedicalRecord(string patientId)
        {
            var patient = this.db.Patients.FirstOrDefault(p => p.Id == patientId);

            if (patient == null)
            {
                patient = this.db.Patients.FirstOrDefault(p => p.UserId == patientId);
            }

            var patientMedicalRecord = this.mapper.Map<PatientTestsAndDiagnosesViewModel>(patient);

            return patientMedicalRecord;
        }

        public PatientProfileViewModel GetPatientById(string patientId)
        {
            var currentPatient = this.db.Patients.FirstOrDefault(p => p.Id == patientId);

            var patient = this.mapper.Map<PatientProfileViewModel>(currentPatient);

            return patient;
        }

        //public PatientProfileViewModel FindPatientByName(string name)
        //{
        //    var patient = this.db.Patients.FirstOrDefault(p => p.User.FirstName + " " + p.User.LastName == name);

        //    PatientProfileViewModel patientProfile = null;

        //    if (patient != null)
        //    {
        //        patientProfile = this.mapper.Map<PatientProfileViewModel>(patient);
        //    }

        //    return patientProfile;
        //}
    }
}
