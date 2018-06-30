using AcTimer.Models;
using AcTimer.Services.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using AcTimer.DTOs;
using static System.Net.WebRequestMethods;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace AcTimer.Controllers.Api
{
    public class StatsController : ApiController
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();
        private ActivityRepository _activityRepository = new ActivityRepository();
        private ApplicationDbContext _context = new ApplicationDbContext();
        private IEnumerable<Statistic> _statistics = new List<Statistic>();

        //GET /api/stats
        [Authorize]
        public IHttpActionResult GetStats()
        {

            return Json(StatsForeachUser(),serializerSettings : new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()} );
        }

        
        #region Helpers

        public IEnumerable<Statistic> StatsForeachUser()
        {
            var myActivitiesByCategory = new List<Statistic>();
            var currentUserName = User.Identity.GetUserName();

            var myActivities = _context.Users.Include(x => x.Activities) // From users table
                .Where(u => u.UserName.Equals(currentUserName)) // get current logged in user
                .Select(a => a.Activities).Single(); //and get his activities

            var categories = _categoryRepository.GetAll();

            foreach (var category in categories)
            {
                var activities = new List<ActivityDto>();
                foreach (var activity in myActivities.Take(10).OrderBy(a => a.Date))
                {
                    if(category.Id == activity.CategoryId)
                    {
                        var activityDto = Mapper.Map<Activity, ActivityDto>(activity);
                        activities.Add(activityDto);
                    }
                }

                if(activities.Count != 0)
                {
                    var statistic = new Statistic
                    {
                        CatgoryName = category.Name,
                        ActivitiesDto = activities,
                        AverageTime = Math.Round( activities.Average(x => x.TotalSpent),2)
                    };
                    myActivitiesByCategory.Add(statistic);
                }
            }
           
            return myActivitiesByCategory;
            
        }
        #endregion
    }//class
}//namespace
