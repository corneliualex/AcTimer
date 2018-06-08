using AcTimer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Models
{
    public class Statistic
    { 
        public string CatgoryName { get; set; }
        public double AverageTime { get; set; }
        public IEnumerable<ActivityDto> ActivitiesDto { get; set; }
    }
}