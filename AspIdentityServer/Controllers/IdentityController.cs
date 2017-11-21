using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspIdentityServer.data.Models;
using AspIdentityServer.data;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;
using AspIdentityServer.data.FormModels;
using System.Security.Claims;
using IdentityModel;

namespace AspIdentityServer.Controllers
{
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Manage accounts")]

    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly ApplicationDBcontext context;


        public IdentityController(
            UserManager<ApplicationUser> usermanager,
            RoleManager<IdentityRole> rolemanager,
            ApplicationDBcontext context)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
            this.context = context;
           
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>IdentityResult</returns>
        // POST: api/identity/Create
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserForm model)
        {

            var user = new ApplicationUser
            {
                givenname = model.givenname,
                familyname = model.familyname,
                AccessFailedCount = 0,
                Email = model.email,
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = model.username.ToUpper(),
                NormalizedUserName = model.username.ToUpper(),
                TwoFactorEnabled = false,
                UserName = model.username
            };


            if (!model.password.Equals(model.passwordvalidate))
            {
                return BadRequest("Passwords don't match");
            }

            var result = await usermanager.CreateAsync(user, model.password);


            if (result.Succeeded)
            {
                var roleresult = await usermanager.AddToRoleAsync(user, model.rolename);

                if (roleresult.Succeeded)
                {
                    var claims = new List<Claim> {
                    new Claim(type: JwtClaimTypes.GivenName, value: user.givenname),
                    new Claim(type: JwtClaimTypes.FamilyName, value: user.familyname),
                     };
                    await usermanager.AddClaimsAsync(user, claims);
                    return Ok();
                }

            }

            return BadRequest("Passwords don't match");

        }  

        
        [HttpGet("users")]
        public async Task<IActionResult> getAllUsers(string rolename)
        {

            var role = await rolemanager.FindByNameAsync(rolename);
            var users = await usermanager.GetUsersInRoleAsync(role.Name);

            foreach (var user in users)
            {
                user.PasswordHash = "";
            }

            return new JsonResult(users);
        }
        
       
    }
}