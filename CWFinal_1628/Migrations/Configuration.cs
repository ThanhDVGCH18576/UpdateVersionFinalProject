namespace CWFinal_1628.Migrations
{
    using CWFinal_1628.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<CWFinal_1628.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CWFinal_1628.Models.ApplicationDbContext";
        }

        protected override void Seed(CWFinal_1628.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                CreateUser(context, "admin@gmail.com", "123456");
                CreateUser(context, "supervisor@gmail.com", "123456");
                CreateUser(context, "builder@gmail.com", "123456");
                CreateUser(context, "investor@gmail.com", "123456");
                CreateUser(context, "salesman@gmail.com", "123456");
                // code create new user in system

                CreateRole(context, "Admin");
                CreateRole(context, "Supervisor");
                CreateRole(context, "Builder");
                CreateRole(context, "Investor");
                CreateRole(context, "Salesman");

                AssignUserToRole(context, "admin@gmail.com", "Admin");
                AssignUserToRole(context, "supervisor@gmail.com", "Supervisor");
                AssignUserToRole(context, "builder@gmail.com", "Builder");
                AssignUserToRole(context, "investor@gmail.com", "Investor");
                AssignUserToRole(context, "salesman@gmail.com", "Salesman");



            }
        }
        private void CreateUser(ApplicationDbContext context, string email, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {

                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var userCreateRusult = userManager.Create(user, password);
            if (!userCreateRusult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateRusult.Errors));
            }
        }

        // code to create role
        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", roleCreateResult.Errors));
            }
        }

        // code to assign user to role
        private void AssignUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join(";", addAdminRoleResult.Errors));
            }
        }
    }
}
