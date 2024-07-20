using FluentAssertions;
using MedicalCenter.Controllers;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Schedules;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System;
using System.Linq;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    
    public class ScheduleControllerTest
    {
        
        
        [Fact]
        public void GetMakeScheduleShouldReturnViewOnlyForDoctors()
            => MyController<ScheduleController>
                .Instance()
                .Calling(c => c.MakeSchedule())
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetMakeScheduleShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Schedule/MakeSchedule")
                .To<ScheduleController>(c => c.MakeSchedule());

        [Fact]
        public void PostMakeScheduleShouldBeMapped()
            => MyRouting
                .Configuration().ShouldMap(request => request
                                       .WithPath("/Schedule/MakeSchedule")
                                       .WithMethod(HttpMethod.Post))
                .To<ScheduleController>(c => c.MakeSchedule(With.Any<InputScheduleFormModel>()));

        //[Theory]
        //[InlineData("06/11/2021")]
        //public void PostMakeScheduleShouldBeOnlyForDoctorsAndShouldRedirectWithValidModel(DateTime date)
        //    => MyPipeline.Configuration()
        //         .ShouldMap(request => request
        //            .WithPath("/Schedule/MakeSchedule")
        //            .WithMethod(HttpMethod.Post)
        //            .WithFormFields(new
        //            {
        //                Date = date
        //            })
        //            .WithUser()
        //            .WithAntiForgeryToken())
        //         .To<ScheduleController>(c => c.MakeSchedule(new InputScheduleFormModel
        //            {
        //                Date = date
        //         }))
        //        .Which()
        //        .ShouldHave()
        //        .ActionAttributes(attributes => attributes
        //            .RestrictingForHttpMethod(HttpMethod.Post)
        //            .RestrictingForAuthorizedRequests("Doctor"))
        //        .ValidModelState()
        //        .Data(data => data
        //           .WithSet<Schedule>(schedule => schedule
        //               .Any(s =>
        //                   s.Date == date)))
        //        .AndAlso()
        //        .ShouldReturn()
        //        .Redirect(redirect => redirect
        //            .To<DoctorsController>(c => c.Manage()));


        [Theory]
        [InlineData("06/11/2021")]
        public void PostMakeScheduleShouldBeOnlyForDoctorsAndShouldRedirectWithValidModel(DateTime date)
            => MyController<ScheduleController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.MakeSchedule(new InputScheduleFormModel
                {
                    Date = date
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests("Doctor"))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Schedule>(schedules => schedules
                        .Any(s =>
                            s.Date == date)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DoctorsController>(c => c.Manage()));

        [Fact]
        public void GetMakeAppiontmentShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Schedule/MakeAppointment/1")
                .To<ScheduleController>(c => c.MakeAppointment(1));


        [Fact]
        public void GetMakeAppiontmentShouldReturnViewOnlyForPatients()
            => MyController<ScheduleController>
                .Instance()
                .Calling(c => c.MakeAppointment(1))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Patient"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetSavedHourShouldReturnViewOnlyForDoctors()
            => MyController<ScheduleController>
                .Instance()
                .Calling(c => c.SavedHour(1))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();


        [Fact]
        public void GetSavedHourShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Schedule/SavedHour/1")
                .To<ScheduleController>(c => c.SavedHour(1));


        [Theory]
        [InlineData("Nothing",1)]
        public void PostMakeAppointmentShouldBeOnlyForPatientsAndShouldRedirectWithValidModel(string reason, int id)
            => MyController<ScheduleController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.MakeAppointment(new SaveHourFormModel { Reason = reason},id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests("Patient"))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Hour>(hours => hours
                        .Any(s =>
                            s.Reason == reason)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DoctorsController>(c => c.AllDoctors()));
    }
}
