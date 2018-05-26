using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            return View();
        }

        public ActionResult Edit(int? id)
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }        

        [HttpPost]
        public ActionResult Save(Activity activity)
        {
            return View();
        }


    }
}