
using MedicalCenter.Services.ViewModels.Schedules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IScheduleService
    {
        Task AddFreeHour(string doctorId, InputScheduleFormModel model);

        ICollection<ListAllSchedulesViewModel> ListAllFreeHours(string doctorId);
        Task SaveHour(int hourId, SaveHourFormModel model, string patientId);

        SavedHourInfoViewModel SavedHourInfo(int id);
    }
}
