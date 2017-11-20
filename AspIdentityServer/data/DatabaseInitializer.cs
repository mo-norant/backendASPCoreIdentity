using AspIdentityServer.data.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AspIdentityServer.data
{
    public class DatabaseInitializer : IDbInitializer
    {

        private readonly ApplicationDBcontext context;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;

        public DatabaseInitializer(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {

            this.usermanager = usermanager;
            this.rolemanager = rolemanager;

        }

        public async Task Initialize(ApplicationDBcontext context)
        {

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded.
            }

            await rolemanager.CreateAsync(new IdentityRole("administrator"));
            await rolemanager.CreateAsync(new IdentityRole("user"));


            var user = new ApplicationUser
            {
                givenname = "mo",
                familyname = "bouzim",
                AccessFailedCount = 0,
                Email = "admin@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "INFO@NORANT.BE",
                NormalizedUserName = "INFO@NORANT.BE",
                TwoFactorEnabled = false,
                UserName = "info@norant.be"
            };

            var result = await usermanager.CreateAsync(user, "Admin01*");
            
        }
    }
}
