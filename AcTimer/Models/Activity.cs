using AcTimer.Models.CustomValidations;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcTimer.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [DateInputRestriction]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name ="Time spent")]
        public TimeSpan TimeSpent { get; set; }

        //fk + nav prop => one to many. many activities have a category
        public int CategoryId { get; set; }
        public string ApplicationUserId { get; set; }

        //fk + nav prop => one to many. many activities are created by an user
        public Category Category { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}