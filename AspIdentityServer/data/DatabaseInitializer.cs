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

        public DatabaseInitializer( UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager )
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
                voornaam = "mo",
                achternaam = "bouzim",
                lievelingskleur = "groen",
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

            if (result.Succeeded)
            {
                var adminUser = await usermanager.FindByNameAsync(user.UserName);
                // Assigns the administrator role.
                await usermanager.AddToRoleAsync(adminUser, "administrator");
                // Assigns claims.
                var claims = new List<Claim> {
                    new Claim(type: JwtClaimTypes.GivenName, value: user.voornaam),
                    new Claim(type: JwtClaimTypes.FamilyName, value: user.achternaam),
                };
                await usermanager.AddClaimsAsync(adminUser, claims);
            }
            else
            {
                Debug.WriteLine(result.ToString());
            }
        }

      


    }
}
