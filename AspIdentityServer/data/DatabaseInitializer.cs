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
        private readonly int COUNT_USERS = 20;

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
            var roleresult = await usermanager.AddToRoleAsync(user, "administrator");


            for (int i = 0; i < COUNT_USERS; i++)
            {


                Random r = new Random();

                IdentityResult resultusers, roleresultusers;

                do
                {
                    var usercreated = new ApplicationUser
                    {
                        givenname = GenerateName(r.Next(4, 10), r),
                        familyname = GenerateName(r.Next(4, 10), r),
                        AccessFailedCount = 0,
                        Email = "admin@gmail.com",
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        NormalizedEmail = "INFO@NORANT.BE",
                        NormalizedUserName = "INFO@NORANT.BE",
                        TwoFactorEnabled = false,
                        UserName = GenerateName(r.Next(6, 11), r)
                    };

                    resultusers = await usermanager.CreateAsync(usercreated, "Admin01*");

                    roleresultusers = await usermanager.AddToRoleAsync(usercreated, "user");

                } while (resultusers.Succeeded && roleresultusers.Succeeded);
            }
        }
        
        private string GenerateName(int len, Random r)
        {
            
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; 
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }


    }
}
