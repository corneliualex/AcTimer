using AcTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AcTimer.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        //Get Activity by Id
        //If user is in role get any activity else get only an activity created by you
        public Activity GetById(int? id)
        {
            if (id == null) return null;
            if (IsUserInRole(_currentUserId))
            {
                return _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).SingleOrDefault(a => a.Id == id);
            }
            else
            {
                return _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser).Where(u => u.ApplicationUserId == _currentUserId).SingleOrDefault(a => a.Id == id);
            }
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities.Include(c => c.Category).Include(u => u.ApplicationUser);
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

        /********************************************* outside the IEntityRepository ************************************/

        public IEnumerable<Activity> GetAllForeachUser()
        {
            return GetAll().Where(u => u.ApplicationUserId == _currentUserId);
        }

        private bool IsUserInRole(string currentUserId)
        {
            var store = new UserStore<ApplicationUser>(_context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = manager.FindById(currentUserId);
            if (manager.IsInRole(currentUserId, "Admin") || manager.IsInRole(_currentUserId, "Moderator"))
                return true;
            return false;
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

    }//class
}//namespace