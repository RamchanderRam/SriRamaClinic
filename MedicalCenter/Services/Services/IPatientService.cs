using MedicalCenter.Services.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IPatientService
    {
        PatientProfileViewModel GetPatientById(string patientId);
        bool IsPatientProfileCompleted(string userId);

        bool IsPatientWithCertainEGNExist(string EGN);
        Task AddPatient(AddPatientFormModel patient, string userId);

        ChangePatientProfileViewModel ChangePatientInfo(string userId);

        Task ChangePatient(ChangePatientProfileViewModel model, string userId);

        PatientProfileViewModel FindPatientByEGN(string egn);

        //PatientProfileViewModel FindPatientByName(string name);

        IEnumerable<PatientProfileViewModel> GetAllPatients();

        PatientTestsAndDiagnosesViewModel GetPatientMedicalRecord(string patientId);
    }
}
