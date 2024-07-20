using MedicalCenter.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class BloodTestsController : Controller
    {
        private readonly IBloodTestsService bloodTestsService;
        private readonly IPatientService patientService;

        public BloodTestsController(IBloodTestsService bloodTestsService, IPatientService patientService)
        {
            this.bloodTestsService = bloodTestsService;
            this.patientService = patientService;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult ViewBloodTests(string id)
        {
            var allFinishedTests = this.bloodTestsService.ListAllFinishedTests(id);

            return View(allFinishedTests);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult SendBloodTests(string id)
        {
            var allParameters = this.bloodTestsService.ListAllParameters();

            this.ViewBag.UserId = id;

            return View(allParameters);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SendBloodTests(string id, List<string> Box)
        {
            var allParameters = this.bloodTestsService.ListAllParameters();

            var currentPatient = this.patientService.GetPatientById(id);

            if (Box.Count == 0)
            {
                TempData["Error"] = "Please select at least one parameter!";
                return this.View(allParameters);
            }

            this.ViewBag.UserId = id;

            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.bloodTestsService.SendBloodTest(Box,doctorId,id);

            return this.Redirect($"/Doctors/PatientProfile?EGN={currentPatient.EGN}");

        }

        
        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult AllBloodTests(string id)
        {
            var allTests = this.bloodTestsService.ListAllUnfinishedTests(id);

            return this.View(allTests);
        }

        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult AllParameters(string id) 
        {
            this.ViewBag.TestId = id;

            var allParams = this.bloodTestsService.AllParametersForSingleTest(id);

            return this.View(allParams);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public async Task<IActionResult> FillTest(double[] parameter,string id)
        {
            var allParams = this.bloodTestsService.AllParametersForSingleTest(id).ToList();

            if (parameter.Length != allParams.Count)
            {
                TempData["Error"] = "Please fill all parameters!";

                return this.Redirect($"/BloodTests/AllParameters/{id}");
            }

            await this.bloodTestsService.FillBloodTest(parameter,id);

            return this.RedirectToAction("Tests","Doctors");
        }

        [Authorize(Roles = "Doctor,Patient")]
        public IActionResult SeeResults(string id)
        {
            var results = this.bloodTestsService.GetSingleResult(id);

            return this.View(results);
        }
    }
}
