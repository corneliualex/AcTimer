using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.Services.EntityRepository
{
    public class CategoryRepository : IEntityRepository<Category>
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public Category getById(int? id)
        {
            if (id == null) { return null; }

            var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            return category;
        }
    }
}