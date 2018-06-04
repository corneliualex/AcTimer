using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Activities.Specification
{
    public class SpecificationByCategoryId : ISpecification<Activity>
    {
        private int? _categoryId;
        public SpecificationByCategoryId(int? categoryId)
        {
            _categoryId = categoryId;
        }

        public bool IsSatisfied(Activity activity)
        {
            if (_categoryId == activity.CategoryId) return true;

            return false;
        }
    }
}