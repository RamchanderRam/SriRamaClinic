using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Schedules;
using MedicalCenter.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Services
{
    public class ScheduleServiceTests
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext mockDatabase;
        public ScheduleServiceTests()
        {
            this.mapper = MockMapper.Instance;

            this.mockDatabase = MockDatabase.Instance;
        }

        [Fact]
        public void MethodShoulReturnAllSchedulesFromDatabase()
        {
            // Arrange
            this.mockDatabase.Schedules.AddRange(GetFakeSchedules());

            this.mockDatabase.SaveChanges();

            var scheduleService = new ScheduleService(this.mapper, this.mockDatabase);

            //Act

            var allSchedules = scheduleService.ListAllFreeHours("xBmKlO76F");

            //Assert

            Assert.Equal(2, allSchedules.Count);
        }

        [Fact]
        public void MethodShouldRetunZeroIfAllOfSchedulesAreWithPreviousDateThanNow()
        {
            // Arrange
            this.mockDatabase.Schedules.AddRange(GetFakeSchedulesWithOlderDate());

            this.mockDatabase.SaveChanges();

            var scheduleService = new ScheduleService(this.mapper, this.mockDatabase);

            //Act

            var allSchedules = scheduleService.ListAllFreeHours("xBmKlO76F");

            //Assert

            Assert.Equal(0, allSchedules.Count);
        }

        [Fact]
        public async Task MethodShouldAddScheduleAndHour()
        {
            //Arrange
            var scheduleService = new ScheduleService(this.mapper, this.mockDatabase);

            //Act

            await scheduleService.AddFreeHour("ddd", new InputScheduleFormModel { Date = new DateTime(2022, 5, 5) });

            var existingSchedule = this.mockDatabase.Schedules.Any(s => s.Date == new DateTime(2022, 5, 5));

            //Assert
            Assert.True(existingSchedule);
        }

        [Fact]
        public async Task MethodSaveHourShouldChangeIsFreeProp()
        {
            //Arrange

            this.mockDatabase.AddRange(GetFakeHours());
            this.mockDatabase.SaveChanges();

            var scheduleService = new ScheduleService(this.mapper, this.mockDatabase);

            SaveHourFormModel model = new SaveHourFormModel
            {
                Reason = "Broken leg"
            };

            //Act

            await scheduleService.SaveHour(4, model,"mmXXcc");

            //Assert

            var savedHour = this.mockDatabase.Hours.FirstOrDefault(h => h.Id == 4);

            Assert.Equal(savedHour.UserId,"mmXXcc");
            Assert.False(savedHour.IsFree);
            Assert.NotNull(savedHour);
        }

        private ICollection<Schedule> GetFakeSchedules()
        {
            return new List<Schedule>()
            {
                new Schedule
                {
                    Id = 1,
                    Date = new DateTime(2027,3,3),
                    UserId = "xBmKlO76F"
                },
                new Schedule
                {
                    Id = 2,
                    Date = new DateTime(2027,3,4),
                    UserId = "xBmKlO76F"
                },
            };
        }

        private ICollection<Schedule> GetFakeSchedulesWithOlderDate()
        {
            return new List<Schedule>()
            {
                new Schedule
                {
                    Id = 1,
                    Date = new DateTime(2021,3,3),
                    UserId = "xBmKlO76F"
                },
                new Schedule
                {
                    Id = 2,
                    Date = new DateTime(2021,3,4),
                    UserId = "xBmKlO76F"
                },
            };
        }

        private ICollection<Hour> GetFakeHours() 
        {
            return new List<Hour>()
            {
                new Hour {Id=4, FreeHour = new DateTime(2026,5,7),ScheduleId=6 },
                new Hour {Id=1, FreeHour = new DateTime(2026,5,8),ScheduleId=3 }
            };

        }
    }
}
