using MedicalCenter.Services.ViewModels.Admin;
using MedicalCenter.Services.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IAdminService
    {
        Task CreateDoctor(CreateDoctorFormModel model);

        ICollection<AllImagesToApproveViewModel> GetAllImagesToApprove();

        Task ApproveImage(string imageId);

        Task DeleteImage(string imageId);

        Task GetPatients(PatientProfileViewModel patientProfileViewModel);

    }
}
