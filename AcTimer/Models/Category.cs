using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }

        //fk + nav prop => one to many. many categories are created by an user
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}