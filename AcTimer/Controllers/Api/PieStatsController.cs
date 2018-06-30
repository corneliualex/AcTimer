using AcTimer.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;

namespace AcTimer.Controllers.Api
{

    public class PieStat
    {
        public string CategoryName { get; set; }
        public double AverageTime { get; set; }
    }

    [Authorize]
    public class PieStatsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<PieStat> Stats(string currentUserId)
        {
            var pieStats = new List<PieStat>();

            //GET current user from db
            var currentUser = _context.Users.Include(a => a.Activities).Single(u => u.Id.Equals(currentUserId));

            var activities = currentUser.Activities.GroupBy(c => c.CategoryId);
            //FORMULA :
            // GET sum of average of activities by category
            // SUM( GROUP ACTIVITIES BY CATEGORY( AVG(TotalSpent) ) )
            var totalHours = activities.Select(a => a.Average(x => x.TotalSpent)).Sum();

            foreach (var activity in activities)
            {
                var category = _context.Categories.Single(c => c.Id == activity.Key);
                pieStats.Add(new PieStat { CategoryName = category.Name, AverageTime = Math.Round(100 * activity.Average(a => a.TotalSpent) / totalHours, 2) });
            }
                       
           
            return pieStats;

        }

        public IHttpActionResult GetPieStats()
        {

            return Json(Stats(User.Identity.GetUserId()), serializerSettings: new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }


    }
}
