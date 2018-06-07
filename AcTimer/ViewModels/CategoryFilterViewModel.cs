using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.ViewModels
{
    public class CategoryFilterViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    }
}