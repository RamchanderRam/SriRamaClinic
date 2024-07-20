

using MedicalCenter.Services.ViewModels.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.Areas.Administrator.Controllers
{

    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }


        public IActionResult GetPatients()
        {
            PatientProfileViewModel patientProfileViewModel = new PatientProfileViewModel();
            return View(patientProfileViewModel);
        }

    }
}
