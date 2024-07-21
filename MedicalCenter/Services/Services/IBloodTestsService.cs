using MedicalCenter.Services.ViewModels.BloodTests;
using MedicalCenter.Services.ViewModels.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IBloodTestsService
    {
        IEnumerable<ListAllParametersViewModel> ListAllParameters();

        Task SendBloodTest(List<string> checkedParams, string doctorId, string patientId);

        IEnumerable<AllBloodTestsViewModel> ListAllUnfinishedTests(string patientId);

        IEnumerable<AllBloodTestsViewModel> ListAllFinishedTests(string patientId);

        IEnumerable<ListAllParametersViewModel> AllParametersForSingleTest(string testId);

        Task FillBloodTest(double[] parameters, string testId);

        IEnumerable<ResultsViewModel> GetSingleResult(string testId);
    }
}
