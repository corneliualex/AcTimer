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
        ///private ApplicationDbContext _context = new ApplicationDbContext();
        private IEntityRepository<Category> _categoryRepository = new CategoryRepository();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return HttpNotFound();

            return View(category);
        }

        //authorize admin & moderator
        [Authorize(Roles ="Admin,Moderator")]
        public ActionResult New()
        {
            return View("CategoryForm", new Category());
        }

        //authorize admin & moderator
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return HttpNotFound();

            return View("CategoryForm", category);
        }

        //authorize admin & moderator
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(int ?id)
        {
            if (_categoryRepository.IsDeleted(id) == false) return HttpNotFound();

            return RedirectToAction("Dashboard","Categories");
        }

        //will add in this view a complete view for the authorized methods
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Dashboard()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }

        //authorize admin & moderator
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Save(Category category)
        {
            _categoryRepository.NewOrUpdate(category);
           
            return RedirectToAction("Dashboard");
        }
    }
}