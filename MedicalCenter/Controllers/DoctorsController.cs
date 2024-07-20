using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Data.Migrations;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Doctors;
using MedicalCenter.Services.ViewModels.Patients;
using MedicalCenter.Services.ViewModels.PRos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{

    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly IScheduleService scheduleService;
        private readonly ApplicationDbContext db;


        public DoctorsController(IDoctorService doctorService, IPatientService patientService, IScheduleService scheduleService)
        {
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.scheduleService = scheduleService;
            //this.db = db;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Add(AddDoctorFormModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Incorrect data format!";
                return this.View();
            }

            await this.doctorService.AddDoctor(model, userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult ViewProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = this.doctorService.GetDoctor(userId);

            return this.View(doctor);
        }

        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult ChangeProfile(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = this.doctorService.GetDoctor(userId);

            return this.View(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public async Task<IActionResult> ChangeProfile(string id, ChangeDoctorInfoFormModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Incorrect data format!";
                return this.View();
            }

            await this.doctorService.ChangeDoctorInfo(id,model);

            return this.RedirectToAction("ViewProfile");
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult Manage()
        {
            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var schedules = this.scheduleService.ListAllFreeHours(doctorId).ToList();

            return this.View(schedules);
        }

        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult FindPatientByEGN()
        {
            return this.View();
        }

        //[HttpPost]
        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult PatientProfile(FindPatientEGNFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var patient = this.patientService.FindPatientByEGN(model.EGN);

            if (patient == null)
            {
                TempData["Error"] = "Patient with that EGN does not exist. Try again!!!";

                return this.View("FindPatientByEGN");
            }

            return this.View(patient);
        }

        
        [Authorize(Roles = "Laboratory Assistant")]
        public IActionResult Tests()
        {
            return this.View("FindPatientByEGN");
        }

        public IActionResult AllDoctors()
        {
            var allDoctors = this.doctorService.GetAllDoctors();

            return this.View(allDoctors);
        }

        public IActionResult ViewDoctor(string id)
        {
            var doctor = this.doctorService.GetDoctorById(id);

            if (doctor == null)
            {
                TempData["Error"] = "Doctor with that id does not exist!!!";

                return this.RedirectToAction("AllDoctors", "Doctors");
            }

            return this.View(doctor);
        }

        public IActionResult SearchByName(SearchDoctorFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "There aren't doctors with the given name!!!";

                return this.RedirectToAction("AllDoctors", "Doctors");
            }

            var docs = this.doctorService.AllMatchedDoctors(model.DoctorName);

            return this.View("AllDoctors",docs);
        }



        //[HttpPost]
        //[Authorize(Roles = "Doctor")]
        public IActionResult AllPatients()
        {
            var allPatients = this.doctorService.GetAllPatients();

            return this.View(allPatients);
        }



        //[HttpPost]
        //[Authorize(Roles = "Doctor")]
        public IActionResult AllPRos()
        {
            var allPRos = this.doctorService.GetAllPRos();

            return this.View(allPRos);
        }


        //[HttpPost]
        //public async Task<IActionResult> CreatePRos(PRo model, string userId)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Incorrect data format!";
        //        return this.View(model);
        //    }

        //    PRo pRo = new PRo
        //    {
        //        Hospital = model.Hospital,
        //        Location = model.Location,
        //        AppointmentDate = model.AppointmentDate,
        //        Reason = model.Reason,
        //        //UserId = userId,
        //    };

        //    try
        //    {
        //        await this.doctorService.AddPro(pRo, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "An unexpected error occurred.";
        //        // Log the exception for debugging purposes
        //        return this.View(model);
        //    }

        //    return this.RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPRos(PRo model)
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (!this.ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Incorrect data format!";
        //        return this.View(model);
        //    }

        //    PRo pRo = new PRo
        //    {
        //        Id = model.Id,
        //        Hospital = model.Hospital ?? "Default Hospital",  // Provide a default value if Hospital is null
        //        AppointmentDate = model.AppointmentDate,
        //        Location = model.Location,
        //        Reason = model.Reason,
        //        //UserId = userId
        //    };

        //    try
        //    {
        //        await this.doctorService.AddPro(pRo, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "An unexpected error occurred.";
        //        // Log the exception for debugging purposes
        //        return this.View(model);
        //    }

        //    return this.RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        public IActionResult AddPRos()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddPRos(PRos pRo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_context.Employees.Add(empobj);
        //        //_context.SaveChanges();
        //        string userId;
        //         this.db.PRo.AddAsync(pRo);
        //         this.db.SaveChangesAsync();

        //        TempData["ResultOk"] = "Record Added Successfully !";
        //        return RedirectToAction("Index");
        //    }

        //    return View(pRo);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPRos(PRo model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                TempData["Error"] = "Incorrect data format!";
                return this.View();
            }

            await this.doctorService.AddPro(model, userId);

            return this.RedirectToAction("AllPRos", "Doctors");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(id);
        }

        //[HttpDelete]
        //[Authorize(Roles = "Doctor")]
        //[HttpPost]
        //public async Task<IActionResult> DeleteProConfirmed(int id)
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    // Additional validation can be performed here if necessary

        //    await this.doctorService.DeletePro(id);

        //    return this.RedirectToAction("AllPRos", "Doctors");
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePro(int id)
        {
            var pRo = new PRo();
            //var pRo = await db.PRo.FindAsync(id);
            if (pRo != null)
            {
                await this.doctorService.DeletePro(id);
                TempData["ResultOk"] = "Record Deleted Successfully!";
            }
            else
            {
                TempData["Error"] = "Record not found.";
            }

            return RedirectToAction("AllPRos");
        }
    }
}
