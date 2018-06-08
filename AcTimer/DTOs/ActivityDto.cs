using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcTimer.DTOs
{
    public class ActivityDto
    {
        //public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Double TotalSpent { get; set; }
        
        //public string ApplicationUserId { get; set; }
        
    }
}