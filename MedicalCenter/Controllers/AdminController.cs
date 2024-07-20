using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult CreateDoctor()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.adminService.CreateDoctor(model);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult AllImages()
        {
            var allImagesToApprove = this.adminService.GetAllImagesToApprove();

            return this.View(allImagesToApprove);
        }

        public async Task<IActionResult> Approve(string id)
        {
            await this.adminService.ApproveImage(id);

            return this.RedirectToAction("AllImages");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.adminService.DeleteImage(id);

            return this.RedirectToAction("AllImages");
        }
    }
}
