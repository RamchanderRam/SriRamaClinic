

using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Services.Common
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now; //Dates Greater than or equal to today are valid (true)

        }
    }
}
