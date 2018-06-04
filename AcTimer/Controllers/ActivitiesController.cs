using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcTimer.ViewModels;
using AcTimer.Services.EntityRepository;
using AcTimer.Services.Filtering;
using AcTimer.Services.Filtering.Activities.Specification;
using AcTimer.Services.Filtering.Activities;

namespace AcTimer.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ActivityRepository _activityRepository = new ActivityRepository();
        private IFilter<Activity> _activitiesFilter = new ActivitiesFilter();


        // GET: Activities
        public ActionResult Index(int? categoryId, DateTime? date, string sortBy, string searchString)
        {
            switch (sortBy)
            {
                case "cateogry":
                    break;
                case "date":
                    break;
                case "hour":
                    break;
                default:
                    break;
            }
            IEnumerable<Activity> activities = _activityRepository.GetAllForeachUser();

            activities = _activitiesFilter.Filter(activities, new SpecificationByDate(date));
            return View(activities);
           
        }

        public ActionResult Details(int? id)
        {
            var activity = _activityRepository.GetById(id);
            if (activity == null) return HttpNotFound();

            return View(activity);
        }

        public ActionResult Edit(int? id)
        {
            var viewModel = _activityRepository.ActivityFormVM(id);
            if (viewModel == null) return HttpNotFound();
            return View("ActivityForm", viewModel);
        }

        public ActionResult Delete(int? id)
        {
            var activityIsDeleted = _activityRepository.IsDeleted(id);
            if (activityIsDeleted == false) return HttpNotFound();

            return RedirectToAction("Index", "Activities");
        }

        public ActionResult New()
        {
            var viewModel = _activityRepository.ActivityFormVM();

            return View("ActivityForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _activityRepository.ActivityFormVM(activity);

                return View("ActivityForm", viewModel);
            }

            if (_activityRepository.IsNewOrUpdate(activity) == false) return HttpNotFound();

            return RedirectToAction("Index", "Activities");
        }

        [Authorize(Roles = ("Admin,Moderator"))]
        public ActionResult Dashboard(int? userId, int? categoryId, DateTime? date, TimeSpan? timeSpent, int? user)
        {
            var activities = _activityRepository.GetAll();

            return View(activities);
        }
    }
}