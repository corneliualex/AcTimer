using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AcTimer.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required()]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateAdded { get; private set; }

        //fk + nav prop => one to many. many categories are created by an user
        public string ApplicationUserId { get; private set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public Category()
        {
            DateAdded = DateTime.Now;
            ApplicationUserId = HttpContext.Current.User.Identity.GetUserId();
        }

        /******************** Not mapped properties *****************************/
        [NotMapped]
        public string ViewTitle { get { return Id == 0 ? "New" : "Edit"; } }
    }
}