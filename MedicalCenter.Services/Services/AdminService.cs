using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Admin;
using MedicalCenter.Services.ViewModels.Parameters;
using MedicalCenter.Services.ViewModels.Patients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IPatientService patientService;

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IMapper mapper, IPatientService patientService)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
            this.patientService = patientService;

        }

        public async Task CreateDoctor(CreateDoctorFormModel model)
        {
            if (await userManager.FindByNameAsync
                           (model.Email) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await userManager.CreateAsync
                (user, model.Password);

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Doctor").Wait();
                }
            }
        }

        //public IEnumerable<PatientProfileViewModel> GetPatients()
        //{
        //    var allParameters = this.db.Parameters.ProjectTo<PatientProfileViewModel>(this.mapper.ConfigurationProvider).ToList();

        //    return allParameters;
        //}


        public async Task GetPatients(PatientProfileViewModel patientProfileViewModel)
        {
            // Implementation of the method based on your logic
            //var patients = await this.db.Patients.ToListAsync();

            //// Assuming you need to set the fetched patients to the provided patientProfileViewModel
            //patientProfileViewModel.Patients = patients;

                          var patientViewModels = await this.db.Patients
                            .ProjectTo<PatientProfileViewModel>(this.mapper.ConfigurationProvider)
                             .ToListAsync();

                       patientProfileViewModel.Patients = patientViewModels;

        }



        public ICollection<AllImagesToApproveViewModel> GetAllImagesToApprove()
        {
            var images = this.db.Images.Where(i => i.IsApproved == false).ProjectTo<AllImagesToApproveViewModel>(this.mapper.ConfigurationProvider).ToList();

            return images;
        }

        public async Task ApproveImage(string imageId)
        {
            var image = this.db.Images.FirstOrDefault(i => i.Id == imageId);

            image.IsApproved = true;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteImage(string imageId)
        {
            var image = this.db.Images.FirstOrDefault(i => i.Id == imageId);

            var doctorWithImage = this.db.Doctors.FirstOrDefault(d => d.UserId == image.UserId);

            doctorWithImage.ImageId = null;

            this.db.Images.Remove(image);

            await this.db.SaveChangesAsync();
        }

        
    }
}
