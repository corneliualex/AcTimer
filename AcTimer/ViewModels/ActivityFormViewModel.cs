using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcTimer.ViewModels
{
    public class ActivityFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        //public Activity Activity { get; set; }
        //properties used in Activity Model
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Time spent")]
        [RegularExpression(@"([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "Invalid hour")]
        public TimeSpan? TimeSpent { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "User")]
        public string ApplicationUserId { get; private set; }
           
        public string ViewTitle { get { return Id == 0 ? "New" : "Edit"; } }

        public ActivityFormViewModel()
        {
            Id = 0;
        }

        public ActivityFormViewModel(Activity activity)
        {
            Id = activity.Id;
            Description = activity.Description;
            Date = activity.Date;
            TimeSpent = activity.TimeSpent;

            CategoryId = activity.CategoryId;
            ApplicationUserId = activity.ApplicationUserId;
        }
    }
}