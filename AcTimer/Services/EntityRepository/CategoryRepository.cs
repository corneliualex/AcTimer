using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AcTimer.Services.EntityRepository
{
    public class CategoryRepository : IEntityRepository<Category>
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //INCLUDE ALL NAV PROPERTIES FROM CATEGORY MODEL TO AVOID NULL REFERENCE
        public Category GetById(int? id)
        {
            if (id == null) { return null; }

            return _context.Categories.Include(u => u.ApplicationUser).Include(a => a.Activities).SingleOrDefault(c => c.Id == id);
        }

        public Category GetDetails(int? id)
        {
            if (id == null) { return null; }

            return GetById(id);
        }

        //INCLUDE ALL NAV PROPERTIES FROM CATEGORY MODEL TO AVOID NULL REFERENCE
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(u => u.ApplicationUser).Include(a => a.Activities).ToList();
        }

        public bool Delete(int? id)
        {
            var category = GetById(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
        
        public void NewOrUpdate(Category category)
        {
            if (category.Id == 0) { _context.Categories.Add(category); }
            else
            {
                var categoryInDb = GetById(category.Id);
                categoryInDb.Name = category.Name;
            }
            _context.SaveChanges();
        }
    }
}