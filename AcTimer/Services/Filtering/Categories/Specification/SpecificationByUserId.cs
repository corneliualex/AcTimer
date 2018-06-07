using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Categories.Specification
{
    public class SpecificationByUserId : ISpecification<Category>
    {
        private string _userId;

        public SpecificationByUserId(string userId)
        {
            _userId = userId;
        }

        public bool IsSatisfied(Category item)
        {
            return _userId.Equals(item.ApplicationUserId);
        }
    }
}