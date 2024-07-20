using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;
        private readonly UserManager<ApplicationUser> userManager;
        public PatientsController(IPatientService patientService, UserManager<ApplicationUser> userManager)
        {
            this.patientService = patientService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddPatientFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Invalid format of data!!";
                return this.View();
            }

            if (this.patientService.IsPatientWithCertainEGNExist(model.EGN))
            {
                TempData["Error"] = "Patient with that EGN already exist!!.";
                return this.View();

            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.patientService.AddPatient(model, userId);

            var user = await this.userManager.GetUserAsync(this.User);

            await this.userManager.AddToRoleAsync(user, "Patient");

            return this.RedirectToAction("ViewProfile", "Patients");

        }

        public IActionResult ViewProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patientInfo = this.patientService.ChangePatientInfo(userId);

            return this.View(patientInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(ChangePatientProfileViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data format!!";
                return this.View();
            }

            await this.patientService.ChangePatient(model, userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Patient,Admin,Doctor")]
        public IActionResult ViewRecord(string id)
        {
            if (this.User.IsInRole("Patient"))
            {
                id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var patientMedicalRecord = this.patientService.GetPatientMedicalRecord(id);

            return this.View(patientMedicalRecord);
        }


    }
}
