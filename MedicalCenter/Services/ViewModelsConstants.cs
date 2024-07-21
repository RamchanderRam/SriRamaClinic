using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services
{
    public static class ViewModelsConstants
    {
        //Doctors
        public const int DoctorPasswordMinLen = 5;
        public const int DoctorFirstNameMaxLen = 20;
        public const int DoctorFirstNameMinLen = 2;
        public const int DoctorLastNameMaxLen = 20;
        public const int DoctorLastNameMinLen = 2;
        public const int DoctorSpecialtyMinLen = 3;
        public const int DoctorSpecialtyMaxLen = 40;
        public const int DoctorBiographyMinLen = 10;

        //Schedules
        public const int SaveHourReasonMinLen = 5;
        public const int SaveHourReasonMaxLen = 100;

        //Diagnose

        public const int DiagnoseMinLen = 5;
        public const int DiagnoseMaxLen = 150;
    }
}
