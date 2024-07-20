using MedicalCenter.Controllers;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Diagnoses;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class DiagnosesControllerTest
    {
        [Fact]
        public void GetWriteDiagnoseShouldReturnViewOnlyForDocs()
            => MyController<DiagnosesController>
                .Instance()
                .Calling(c => c.WriteDiagnose("dd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetWriteDiagnoseShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Diagnoses/WriteDiagnose/dd")
               .To<DiagnosesController>(c => c.WriteDiagnose("dd"));

        [Theory]
        [InlineData("Pneumonia")]
        public void PostWriteDiagnoseShouldBeOnlyForPatientsAndRedirectWithValidModel(string diagnose)
            => MyController<DiagnosesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.WriteDiagnose("dd", new DiagnoseFormModel
                {
                    Diagnose = diagnose

                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests("Doctor"))
                .ValidModelState()
                .Data(data => data
                    .WithSet<MedicalExamination>(me => me
                        .Any(d =>
                            d.Diagnose == diagnose)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DoctorsController>(c => c.Manage()));
    }
}
