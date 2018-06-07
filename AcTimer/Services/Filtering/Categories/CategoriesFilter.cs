using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Categories
{
    public class CategoriesFilter : IFilter<Category>
    {
        public IEnumerable<Category> Filter(IEnumerable<Category> items, ISpecification<Category> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item)) yield return item;
            }
        }
    }
}