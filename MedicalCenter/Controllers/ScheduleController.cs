using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Schedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly IDoctorService doctorService;

        public ScheduleController(IScheduleService scheduleService, IDoctorService doctorService)
        {
            this.scheduleService = scheduleService;
            this.doctorService = doctorService;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult MakeSchedule()
        {

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> MakeSchedule(InputScheduleFormModel model)
        {
            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {

                return this.View();
            }

            await this.scheduleService.AddFreeHour(doctorId, model);

            return this.RedirectToAction("Manage", "Doctors");
        }


        [Authorize(Roles = "Patient")]
        public IActionResult FreeHours(string id)
        {
            var allFreeHours = this.scheduleService.ListAllFreeHours(id);

            var currentDoc = this.doctorService.GetDoctorById(id);

            this.ViewBag.DoctorFullName = currentDoc.FullName;

            return this.View(allFreeHours);
        }

        [Authorize(Roles = "Patient")]
        public IActionResult MakeAppointment(int id)
        {
            this.ViewBag.HourId = id;

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> MakeAppointment(SaveHourFormModel model, int id)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    TempData["Error"] = "Incorrect data format!";
            //    return this.View();
            //}

            var patientId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.scheduleService.SaveHour(id, model, patientId);

            return this.RedirectToAction("AllDoctors", "Doctors");
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult SavedHour(int id)
        {
            var hourInfo = this.scheduleService.SavedHourInfo(id);

            return this.View(hourInfo);
        }
    }
}
