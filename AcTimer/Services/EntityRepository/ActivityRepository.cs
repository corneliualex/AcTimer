using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AcTimer.ViewModels;
using Microsoft.AspNet.Identity;

namespace AcTimer.Services.EntityRepository
{
    public class ActivityRepository : IEntityRepository<Activity>
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private string _currentUserId;

        public ActivityRepository()
        {
            _currentUserId = HttpContext.Current.User.Identity.GetUserId();
        }

        public Activity GetById(int? id)
        {
            if (id == null) return null;

            return _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).Where(u => u.ApplicationUserId == _currentUserId).SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).ToList().Where(u => u.ApplicationUserId == _currentUserId);
        }

        public bool IsDeleted(int? id)
        {
            var activity = GetById(id);
            if (activity == null) return false;

            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return true;
        }

        public bool IsNewOrUpdate(Activity activity)
        {
            if (activity.Id == 0)
            {
                activity.ApplicationUserId = HttpContext.Current.User.Identity.GetUserId();
                _context.Activities.Add(activity);
            }
            else
            {
                var activityInDb = GetById(activity.Id);
                if (activityInDb == null) return false;

                activityInDb.Description = activity.Description;
                activityInDb.Date = activity.Date;
                activityInDb.TimeSpent = activity.TimeSpent;
                activityInDb.CategoryId = activity.CategoryId;

            }
            _context.SaveChanges();
            return true;
        }

        public ActivityFormViewModel ActivityFormVM()
        {
            var viewModel = new ActivityFormViewModel()
            {
                Categories = _context.Categories.ToList()
            };

            return viewModel;
        }

        public ActivityFormViewModel ActivityFormVM(int? id)
        {
            var activity = GetById(id);
            if (activity == null) return null;
            var viewModel = new ActivityFormViewModel(activity)
            {
                Categories = _context.Categories.ToList()
            };

            return viewModel;
        }

        public ActivityFormViewModel ActivityFormVM(Activity activity)
        {
            var viewModel = new ActivityFormViewModel(activity)
            {
                Categories = _context.Categories.ToList()
            };

            return viewModel;
        }


    }
}