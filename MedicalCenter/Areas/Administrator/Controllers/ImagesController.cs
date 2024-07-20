using MedicalCenter.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicalCenter.Areas.Administrator.Controllers
{
    public class ImagesController : AdministratorController
    {
        private readonly IAdminService adminService;

        public ImagesController(IAdminService adminService)
        {
            this.adminService = adminService;
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
