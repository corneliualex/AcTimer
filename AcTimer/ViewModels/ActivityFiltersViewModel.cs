using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.ViewModels
{
    
    public class ActivityFiltersViewModel
    {
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public string SortBy { get; set; }
    }
}