using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Diagnoses;
using MedicalCenter.Services.ViewModels.Doctors;
using MedicalCenter.Services.ViewModels.Patients;
using MedicalCenter.Services.ViewModels.PRos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public DoctorService(ApplicationDbContext db, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task AddDoctor(AddDoctorFormModel model, string userId)
        {
            string extension = Path.GetExtension(model.Image.FileName);

            Image image = new Image()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                Extension = extension
            };

            Doctor doctor = new Doctor()
            {
                Specialty = model.Specialty,
                Biography = model.Biography,
                UserId = userId,
                Image = image
            };

            await this.db.Doctors.AddAsync(doctor);
            await this.db.SaveChangesAsync();

            this.SavePicture(model.Image, image.Id);
        }

        public async Task ChangeDoctorInfo(string userId, ChangeDoctorInfoFormModel model)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == userId);

            string extension = Path.GetExtension(model.Image.FileName);

            Image image = new Image()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                Extension = extension
            };

            await this.db.Images.AddAsync(image);
            await this.db.SaveChangesAsync();

            doctor.ImageId = image.Id;
            doctor.Biography = model.Biography;

            await this.db.SaveChangesAsync();

            this.SavePicture(model.Image, image.Id);


        }

        public PreviewDoctorProfileViewModel GetDoctor(string userId)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == userId);

            var doctorsInfo = this.mapper.Map<PreviewDoctorProfileViewModel>(doctor);

            return doctorsInfo;
        }

        public bool IsDoctorProfileCompleted(string userId)
        {
            return this.db.Doctors.Any(d => d.UserId == userId);
        }

        private void SavePicture(IFormFile image, string pictureName)
        {
            string uploadsFolder = Path.Combine(this.hostEnvironment.WebRootPath, "images");

            string extension = Path.GetExtension(image.FileName);

            string pictureFileName = pictureName + extension;

            string filePath = Path.Combine(uploadsFolder, pictureFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

        }


        public IEnumerable<ListAllDoctorsViewModel> GetAllDoctors()
        {
            var allDoctors = this.db.Doctors.ProjectTo<ListAllDoctorsViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allDoctors;
        }

        public async Task WriteDiagnose(string patientId, string doctorId, DiagnoseFormModel model)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == doctorId);

            var medicalExamination = new MedicalExamination()
            {
                Diagnose = model.Diagnose,
                Date = DateTime.UtcNow,
                PatientId = patientId,
                Doctor = doctor
            };

            await this.db.MedicalExamination.AddAsync(medicalExamination);
            await this.db.SaveChangesAsync();
        }

        public SingleDoctorViewModel GetDoctorById(string doctorId)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == doctorId);

            var doctorsInfo = this.mapper.Map<SingleDoctorViewModel>(doctor);

            return doctorsInfo;
        }

        public IEnumerable<ListAllDoctorsViewModel> AllMatchedDoctors(string fullName)
        {
            var allMatchedDocs = this.db.Doctors.Where(d => d.User.FirstName.Contains(fullName) || d.User.LastName.Contains(fullName)).ProjectTo<ListAllDoctorsViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allMatchedDocs;
        }


        public IEnumerable<PatientProfileViewModel> GetAllPatients()
        {
            var allPatient = this.db.Patients.ProjectTo<PatientProfileViewModel>(this.mapper.ConfigurationProvider).ToList();

            return allPatient;
        }

        public IEnumerable<PRo> GetAllPRos()
        {
            
            var sqlQuery = "SELECT Id, Hospital, Location, AppointmentDate,UserId,  Reason FROM PRos";
            var allPRos = db.Set<PRos>().FromSqlRaw(sqlQuery).ToList();

            var result = allPRos.Select(pros => new PRo
            {
                Id = pros.Id,
                HospitalName = pros.Hospital,
                Location = pros.Location,
                AppointmentDate = pros.AppointmentDate,
                Reason = pros.Reason
            });

            return result;
        }

        public async Task AddPro(PRo model, string userId)
        {
          
            PRos doctor = new PRos()
            {
                Hospital = model.HospitalName,
                Location = model.Location,
                AppointmentDate = model.AppointmentDate,
                Reason = model.Reason,
                UserId = userId,
            };

            await this.db.PRo.AddAsync(doctor); // Ensure the correct DbSet name
            await this.db.SaveChangesAsync();
        }


        public async Task DeletePro(int id)
        {
            var proToDelete = await db.Set<PRos>().FirstOrDefaultAsync(pro => pro.Id == id);
            if (proToDelete != null)
            {
                db.Set<PRos>().Remove(proToDelete);
                await db.SaveChangesAsync();
            }
        }

    }
}
