using AcTimer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcTimer.Services.EntityRepository;
using AcTimer.Services.Filtering.Categories;
using AcTimer.Services.Filtering.Categories.Specification;
using AcTimer.Services.Filtering;
using AcTimer.ViewModels;

namespace AcTimer.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private CategoriesFilter _categoriesFilter = new CategoriesFilter();
        private IEntityRepository<Category> _categoryRepository = new CategoryRepository();

        // GET: Categories
        public ActionResult Index(DateTime? date, string userId)
        {
            var viewModel = new CategoryFilterViewModel
            {
                Categories = _categoryRepository.GetAll(),
                ApplicationUsers = _context.Users.ToList()
            };

            viewModel.Categories = GetCategoriesFiltered(viewModel.Categories, date, userId);
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null) return HttpNotFound();

            return View(category);
        }

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
        public ActionResult Delete(int? id)
        {
            if (_categoryRepository.IsDeleted(id) == false) return HttpNotFound();

            return RedirectToAction("Dashboard", "Categories");
        }

        //will add in this view a complete view for the authorized methods
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Dashboard(DateTime? date, string userId)
        {
            var viewModel = new CategoryFilterViewModel
            {
                Categories = _categoryRepository.GetAll(),
                ApplicationUsers = _context.Users.ToList()
            };

            viewModel.Categories = GetCategoriesFiltered(viewModel.Categories, date, userId);
            return View(viewModel);
        }

        //authorize admin & moderator
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category category)
        {
            if (_categoryRepository.IsNewOrUpdate(category) == false) return HttpNotFound();

            if (User.IsInRole("Admin") | User.IsInRole("Moderator"))
                return RedirectToAction("Dashboard");
            else return RedirectToAction("Index");
        }


        private IEnumerable<Category> GetCategoriesFiltered(IEnumerable<Category> categories, DateTime? date, string userId)
        {
            if (date != null && String.IsNullOrEmpty(userId))
            {
                categories = _categoriesFilter.Filter(categories, new SpecificationByDate(date));
            }
            else if (!String.IsNullOrEmpty(userId) && date == null)
            {
                categories = _categoriesFilter.Filter(categories, new SpecificationByUserId(userId));
            }
            else if (!String.IsNullOrEmpty(userId) && date != null)
            {
                categories = _categoriesFilter.Filter(categories, new DoubleSpecification<Category>(new SpecificationByDate(date), new SpecificationByUserId(userId)));
            }

            return categories;

        }
    }//class
}//namespace