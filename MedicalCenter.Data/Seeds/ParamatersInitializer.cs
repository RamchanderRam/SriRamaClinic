using MedicalCenter.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Data.Seeds
{
    public class ParamatersInitializer
    {
        public static Parameter[] SeedParams()
        {
            string[] parameterNames = { "Hemoglobin", "Red cell count", "Hematocrit", "MCV", "MCH", "MCHC", "WBC", "Lymphocytes", "Neutrophils", "Monocytes", "Basophils", "Eosinophils", "Plateles count", "RDW", "Total Cholesterol", "LDL", "HDL", "Triglycerides", "CRP", "Creatinine", "ASAT", "ALAT", "GGT", "Erythrocyte" };

            double[] minValues = { 11, 3.12, 35.4, 90.4, 26, 25.8, 3.1, 15, 15, 1, 0, 0, 152, 11.4, 5.3, 3.36, 1.03, 1.69, 0.65, 0.7, 15, 9, 11, 5 };

            double[] maxValues = { 17.3, 7.3, 56.5, 128, 41.1, 33.6, 21.6, 75, 78, 14, 2, 2, 472, 21.5, 6.2, 4.11, 1.55, 2.25, 3, 1.2, 36, 45, 58, 8.1 };

            List<Parameter> parameters = new List<Parameter>();

            for (int i = 0; i < parameterNames.Length; i++)
            {
                Parameter parameter = new Parameter()
                {
                    Id = i + 1,
                    Name = parameterNames[i],
                    MinValue = minValues[i],
                    MaxValue = maxValues[i]
                };

                parameters.Add(parameter);
            }

            return parameters.ToArray();
        }
    }
}
