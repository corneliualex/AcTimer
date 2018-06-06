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
        private CategoryRepository _categoriesRepository = new CategoryRepository();
        private IFilter<Activity> _activitiesFilter = new ActivitiesFilter();

        // GET: Activities
        public ActionResult Index(int? categoryId, DateTime? date, string searchString)
        {
            var viewModel = new ActivityFiltersViewModel
            {
                Activities = _activityRepository.GetAllForeachUser(),
                Categories = _categoriesRepository.GetAll(),
            };

            viewModel.Activities = GetActivitiesFiltered(viewModel.Activities, categoryId, date);

            return View(viewModel);

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
        public ActionResult Dashboard(int? categoryId, DateTime? date, string userId)
        {
            var viewModel = new ActivityFiltersViewModel
            {
                Activities = _activityRepository.GetAll(),
                Categories = _categoriesRepository.GetAll(),
            };

            if (userId == null)
            {
                viewModel.Activities = GetActivitiesFiltered(viewModel.Activities, categoryId, date);
            }
            else
            {
                viewModel.Activities = GetActivitiesFiltered(viewModel.Activities, categoryId, date, userId);
            }


            return View(viewModel);
        }

        private IEnumerable<Activity> GetActivitiesFiltered(IEnumerable<Activity> activities, int? categoryId, DateTime? date)
        {

            if (categoryId != null && date == null)
            {
                activities = _activitiesFilter.Filter(activities, new SpecificationByCategoryId(categoryId));
            }
            else if (date != null && categoryId == null)
            {
                activities = _activitiesFilter.Filter(activities, new SpecificationByDate(date));
            }
            else if (categoryId != null && date != null)
            {
                activities = _activitiesFilter.Filter(activities, new DoubleSpecification<Activity>(new SpecificationByCategoryId(categoryId), new SpecificationByDate(date)));
            }

            return activities;
        }

        private IEnumerable<Activity> GetActivitiesFiltered(IEnumerable<Activity> activities, int? categoryId, DateTime? date, string userId)
        {
            if (userId != null && categoryId == null && date == null)
            {
                //filter foruserId
            }
            else if (userId != null && categoryId != null && date == null)
            {
                //filter user and date
            }
            else if (userId != null && date != null && categoryId == null)
            {
                //filter user and category 
            }
            else if (categoryId != null && date != null && categoryId != null)
            {
                // Triple Specification
                
            }

            return activities;
        }

    }
}