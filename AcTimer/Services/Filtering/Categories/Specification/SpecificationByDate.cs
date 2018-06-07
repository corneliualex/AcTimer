using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Categories.Specification
{
    public class SpecificationByDate : ISpecification<Category>
    {
        private DateTime? _date;

        public SpecificationByDate(DateTime? date)
        {
            _date = date;
        }

        public bool IsSatisfied(Category item)
        {
            return _date.Value.ToShortDateString().Equals(item.DateAdded.ToShortDateString());
        }
    }
}