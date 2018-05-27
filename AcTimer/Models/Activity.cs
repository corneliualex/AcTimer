using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSpent { get; set; }

        //fk + nav prop => one to many. many activities have a category
        public int CategoryId { get; set; }
        public string ApplicationUserId { get; private set; }

        //fk + nav prop => one to many. many activities are created by an user
        public Category Category { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        public Activity()
        {
            ApplicationUserId = HttpContext.Current.User.Identity.GetUserId();
        }
    }
}