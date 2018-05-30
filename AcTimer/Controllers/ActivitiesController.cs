using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcTimer.ViewModels;
using AcTimer.Services.EntityRepository;

namespace AcTimer.Controllers
{
    public class ActivitiesController : Controller
    {
        private ActivityRepository _activityRepository = new ActivityRepository();


        // GET: Activities
        public ActionResult Index()
        {
            var activities = _activityRepository.GetAll();
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
            //var activity = _activityRepository.GetById(id);
            //if (activity == null) return HttpNotFound();

            var viewModel = _activityRepository.ActivityFormVM(id);
            
            return View("ActivityForm", viewModel);
        }

        public ActionResult Delete(int? id)
        {
            var activityIsDeleted = _activityRepository.IsDeleted(id);
            if (activityIsDeleted ==  false) return HttpNotFound();
          
            return RedirectToAction("Index","Activities");
        }

        public ActionResult New()
        {
            var viewModel = _activityRepository.ActivityFormVM();
       
            return View("ActivityForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = _activityRepository.ActivityFormVM(activity);

                return View("ActivityForm",viewModel);
            }

            _activityRepository.NewOrUpdate(activity);
           
            return RedirectToAction("Index", "Activities");
        }

    }
}