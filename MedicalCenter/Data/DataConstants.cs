using System;
using System.Collections.Generic;

namespace MedicalCenter.Data
{
    public static class DataConstants
    {
        //Users
        public const int UserFirstNameMaxLen = 25;
        public const int UserLastNameMaxLen = 25;

        //Doctors
        public const int DoctorSpecialtyMaxLen = 50;

        //Patients
        public const int PatientCountryMaxLen = 30;
        public const int PatientCountryMinLen = 3;
        public const int PatientTownMaxLen = 15;
        public const int PatientTownMinLen = 3;
        public const int PatientAddressMaxLen = 100;
        public const int PatientAddressMinLen = 5;
        public const int PatientEGNMaxLen = 10;
        public const string PatientEGNPattern = @"^[0-9]{10}$";

        //Hours
        public const int HourReasonMaxLen = 100;

        //Parameters
        public const int ParameterNameMaxLen = 40;

        //MedicalExamination

        public const int ExaminationDiagnoseMaxLen = 150;
        public const int ExaminationDiagnoseMinLen = 5;

    }
}
