using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.Filtering.Activities
{
    public class ActivitiesFilter : IFilter<Activity>
    {
        public IEnumerable<Activity> Filter(IEnumerable<Activity> activities, ISpecification<Activity> specification)
        {
            foreach (var activity in activities)
            {
                if (specification.IsSatisfied(activity)) yield return activity;
            }
        }
    }
}