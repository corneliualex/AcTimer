using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcTimer.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        
        public DateTime DateAdded { get; private set; }
        
        public string ApplicationUserId { get; set; }

        public ICollection<ActivityDto> Activities { get; set; }
    }
}