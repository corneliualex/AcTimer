using AcTimer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcTimer.Services.EntityRepository;

namespace AcTimer.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private IEntityRepository<Category> repository = new CategoryRepository();

        // GET: Categories
        public ActionResult Index()
        {
            return View(_context.Categories.Include(u => u.ApplicationUser).ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null) { return HttpNotFound(); }

            var category = _context.Categories.Include(u => u.ApplicationUser).SingleOrDefault(c => c.Id == id);
            if(category == null) { return HttpNotFound(); }
                      
            return View(category);
        }

        //authorize admin & moderator
        public ActionResult New()
        {
            return View("CategoryForm", new Category());
        }

        //authorize admin & moderator
        public ActionResult Edit(int? id)
        {
            var category = repository.getById(id);
            if (category == null) return HttpNotFound();

            return View("CategoryForm", category);
        }

        //authorize admin & moderator
        public ActionResult Delete(int ?id)
        {
            if (id == null) HttpNotFound();
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return HttpNotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index","Categories");
        }

        //will add in this view a complete view for the authorized methods
        public ActionResult Dashboard()
        {
            return View(_context.Categories.Include(u => u.ApplicationUser).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //authorize admin & moderator
        public ActionResult Save(Category category)
        {
            if (category.Id == 0) { _context.Categories.Add(category); }
            else
            {
                var categoryInDb = _context.Categories.Single(c => c.Id == category.Id);
                categoryInDb.Name = category.Name;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}