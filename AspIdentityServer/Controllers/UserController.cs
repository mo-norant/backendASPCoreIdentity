using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspIdentityServer.data.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace AspIdentityServer.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access resources")]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;




        public UserController(UserManager<ApplicationUser> usermanager,
            RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
           
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await usermanager.GetUserAsync(User);

            var roleresult = await usermanager.GetRolesAsync(user);

            if(user != null)
            {
                user.PasswordHash = "";
                user.ConcurrencyStamp = "";

                return new JsonResult(user);
            }

            return BadRequest("usermanager returns null or bad request");
            
        }



        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser()
        {
            
            var user = await usermanager.GetUserAsync(User);
            var result = await usermanager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);

            }
            return BadRequest();
        }


        

        
    }
}