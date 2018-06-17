using AcTimer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace AcTimer.Controllers
{
  
    [Authorize(Roles ="Admin")]
    public class UsersController : AccountController
    {
        private AccountController _accountController = new AccountController();
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Delete(int? id)
        {
            return View();
        }
    }
}