using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Activities.Specification
{
    public class SpecificationByDate : ISpecification<Activity>
    {
        private DateTime? _date;

        public SpecificationByDate(DateTime? date)
        {
            _date = date;
        }

        public bool IsSatisfied(Activity activity)
        {
            if (_date.HasValue && _date.Value.ToString("dd/mm/yyyy").Equals(activity.Date.ToString("dd/mm/yyyy"))) return true;

            return false;
        }
    }
}