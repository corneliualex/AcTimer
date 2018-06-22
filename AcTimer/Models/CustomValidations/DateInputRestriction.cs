using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcTimer.Models.CustomValidations
{
    public class DateInputRestriction : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var activity = (Activity)validationContext.ObjectInstance;
            if(activity.Id == 0)
            {
                if (activity.Date == null)
                    return new ValidationResult("Date is required");
                else
           if ((DateTime.Now - activity.Date).Days > 3)
                    return new ValidationResult("Activity is older than 3 days");
                else
           if (DateTime.Now < activity.Date)
                    return new ValidationResult("Really ?");
                else
                    return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
           
    }
}