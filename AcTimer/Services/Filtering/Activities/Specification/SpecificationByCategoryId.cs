using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Activities.Specification
{
    public class SpecificationByCategoryId : ISpecification<Activity>
    {
        public int? categoryId;
        public SpecificationByCategoryId(int? categoryId)
        {
            this.categoryId = categoryId;
        }

        public bool IsSatisfied(Activity activity)
        {
            if (categoryId == activity.CategoryId) return true;
            return false;
        }
    }
}