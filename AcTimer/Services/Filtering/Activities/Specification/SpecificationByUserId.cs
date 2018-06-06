using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Activities.Specification
{
    public class SpecificationByUserId : ISpecification<Activity>
    {
        private string _userId;

        public SpecificationByUserId(string userId)
        {
            _userId = userId;
        }

        public bool IsSatisfied(Activity item)
        {
            if (_userId.Equals(item.ApplicationUserId)) return true;
            return false;
        }
    }
}