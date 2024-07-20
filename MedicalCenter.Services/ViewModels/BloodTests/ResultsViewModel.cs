using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.BloodTests
{
    public class ResultsViewModel
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public string HighLow { get; set; }
    }
}
