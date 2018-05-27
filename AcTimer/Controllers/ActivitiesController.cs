using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcTimer.ViewModels;

namespace AcTimer.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            IEnumerable<Activity> categories = _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).ToList();
            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return HttpNotFound();

            var activity = _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).SingleOrDefault(a => a.Id == id);
            if (activity == null) return HttpNotFound();

            return View(activity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var activity = _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).SingleOrDefault(a => a.Id == id);
            if (activity == null) return HttpNotFound();

            var viewModel = new ActivityFormViewModel(activity)
            {
                Categories = _context.Categories.ToList()
            };

            return View("ActivityForm", viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new ActivityFormViewModel()
            {
                Categories = _context.Categories.ToList()
            };

            return View("ActivityForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Activity activity)
        {
            if (activity.Id == 0) _context.Activities.Add(activity);
            else
            {
                var activityInDb = _context.Activities.SingleOrDefault(a => a.Id == activity.Id);

                activityInDb.Description = activity.Description;
                activityInDb.Date = activity.Date;
                activityInDb.TimeSpent = activity.TimeSpent;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Activities");
        }


    }
}