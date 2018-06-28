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
using System.Web.Security;

namespace AcTimer.Controllers
{
    class UserRoles
    {
        public IdentityUser User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

    [Authorize(Roles = "Admin")]
    public class UsersController : AccountController
    {
        private AccountController _accountController = new AccountController();
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            List<UserRoles> _userRoles = new List<UserRoles>();

            var userIds = _context.UserRoles.Select(c => c.UserId);
            foreach (var id in userIds)
            {
                var user = UserManager.FindById(id);
                var roles = UserManager.GetRoles(id);
                _userRoles.Add(new UserRoles { User = user, Roles = roles });
            }
            return View(_userRoles);
        }

        public ActionResult Delete(int? id)
        {
            return View();
        }


        public ActionResult New()
        {
            var viewModel = new RegisterAccountViewModel { Roles = _context.Roles.ToList() };
            return View("UsersForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUser(RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                //create user first
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                //if user is created
                if (result.Succeeded)
                {
                    //add role to user
                    var store = new RoleStore<IdentityRole>(_context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = manager.FindById(model.IdentityRoleId);

                    UserManager.AddToRole(user.Id, role.Name);
                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            model.Roles = _context.Roles.ToList();
            return View("UsersForm", model);
        }
    }
}