using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.ViewModels
{
    public class ActivityFormViewModel
    {
        //public Activity Activity { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSpent { get; set; }

        //fk + nav prop => one to many. many activities have a category
        public int CategoryId { get; set; }
        public string ApplicationUserId { get; private set; }

        //fk + nav prop => one to many. many activities are created by an user
        //public Category Category { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

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