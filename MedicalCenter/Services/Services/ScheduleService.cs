using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Schedules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ScheduleService(IMapper mapper, ApplicationDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }

        public async Task AddFreeHour(string doctorId, InputScheduleFormModel model)
        {
            var dayFormat = DateTime.ParseExact(model.Date.ToString("MM.dd.yyyy"), "MM.dd.yyyy", CultureInfo.InvariantCulture);

            var hourFormat = DateTime.ParseExact(model.Date.ToString("HH:mm"), "HH:mm", CultureInfo.InvariantCulture);

            var schedule = this.db.Schedules.FirstOrDefault(d => d.UserId == doctorId && d.Date == dayFormat);

            if (schedule == null)
            {
                schedule = new Schedule()
                {
                    Date = dayFormat,
                    UserId = doctorId
                };

                await this.db.Schedules.AddAsync(schedule);
                await this.db.SaveChangesAsync();
            }

            var hour = this.db.Hours.FirstOrDefault(h => h.ScheduleId == schedule.Id && h.FreeHour == hourFormat);

            if (hour == null)
            {
                hour = new Hour()
                {
                    FreeHour = hourFormat,
                    ScheduleId = schedule.Id
                };

                await this.db.Hours.AddAsync(hour);
                await this.db.SaveChangesAsync();
            }
        }

        public ICollection<ListAllSchedulesViewModel> ListAllFreeHours(string doctorId)
        {
            var schedules = this.db.Schedules.Where(d => d.UserId == doctorId && d.Date > DateTime.UtcNow).OrderBy(h => h.Date).ProjectTo<ListAllSchedulesViewModel>(this.mapper.ConfigurationProvider).ToList();

            return schedules;
        }

        public SavedHourInfoViewModel SavedHourInfo(int id)
        {
            var hour = this.db.Hours.FirstOrDefault(h => h.Id == id);

            var savedHourInfo = this.mapper.Map<SavedHourInfoViewModel>(hour);

            return savedHourInfo;
        }

        public async Task SaveHour(int hourId, SaveHourFormModel model, string patientId)
        {
            var currentHour = this.db.Hours.FirstOrDefault(h => h.Id == hourId);

            if (currentHour == null)
            {
                // Handle the case when no Hour is found with the provided hourId
                throw new ArgumentException($"No hour found with ID {hourId}");
            }

            currentHour.UserId = patientId;
            currentHour.Reason = model.Reason;
            currentHour.IsFree = false;

            await this.db.SaveChangesAsync();
        }
    }
}
