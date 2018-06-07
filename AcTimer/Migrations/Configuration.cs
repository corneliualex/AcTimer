namespace AcTimer.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AcTimer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AcTimer.Models.ApplicationDbContext context)
        {
            //seeding app with a role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            //seeding app with a user and add a role to it
            var userNameAndEmail = "admin@app.com";
            if (!context.Users.Any(u => u.UserName == userNameAndEmail))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = userNameAndEmail, UserName = userNameAndEmail };

                manager.Create(user, "Admin1!");
                manager.AddToRole(user.Id, "Admin");

                //seed db when admin is already created
                context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Fotball", ApplicationUserId = user.Id },
                new Category() { Name = "Relaxing", ApplicationUserId = user.Id },
                new Category() { Name = "Gaming", ApplicationUserId = user.Id },
                new Category() { Name = "Working", ApplicationUserId = user.Id }
                );
            }




        }


        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data. E.g.
        //
        //    context.People.AddOrUpdate(
        //      p => p.FullName,
        //      new Person { FullName = "Andrew Peters" },
        //      new Person { FullName = "Brice Lambson" },
        //      new Person { FullName = "Rowan Miller" }
        //    );
        //
    }
}

