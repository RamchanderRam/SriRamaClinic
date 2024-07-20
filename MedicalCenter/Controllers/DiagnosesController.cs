using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Diagnoses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly IDoctorService doctorService;

        public DiagnosesController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult WriteDiagnose(string id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> WriteDiagnose(string id, DiagnoseFormModel model)
        {
            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Diagnose format is incorrect!!";
                return this.View();
            }

            await this.doctorService.WriteDiagnose(id,doctorId,model);

            return this.RedirectToAction("Manage", "Doctors");
        }
    }
}
