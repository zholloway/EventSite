namespace EventSite.Migrations
{
    using EventSite.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventSite.Models.ApplicationDbContext context)
        {
            var userRole = "user";
            var adminRole = "admin";

            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            //create user role if not already in db
            if(!context.Roles.Any(a => a.Name == userRole))
            {
                var role = new IdentityRole { Name = userRole };
                manager.Create(role);
            }

            //create admin role if not already in db
            if (!context.Roles.Any(a => a.Name == adminRole))
            {
                var role = new IdentityRole { Name = adminRole };
                manager.Create(role);
            }

            //create default admin for site
            var defaultAdmin = "admin@localbar.com";
            var password = "password";

            if(!context.Users.Any(a => a.UserName == defaultAdmin))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = defaultAdmin };

                userManager.Create(user, password);
                userManager.AddToRole(user.Id, adminRole);
            }
        }
    }
}
