using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspIdentityServer.data;
using AspIdentityServer.data.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;

namespace AspIdentityServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access resources")]

    public class PostsController : Controller
    {
        private readonly ApplicationDBcontext context;
        private readonly UserManager<ApplicationUser> usermanager;

        public PostsController(ApplicationDBcontext context, UserManager<ApplicationUser> usermanager)
        {
            this.context = context;
            this.usermanager = usermanager;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            var user = await usermanager.GetUserAsync(User);
            var posts = (from post in context.Post
                         where post.ID == user.Id
                         select new Post
                         {
                             content = post.content,
                             dateTime = post.dateTime,
                             PostID = post.PostID
                             
                         }).ToList();


            return new JsonResult(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await context.Post.SingleOrDefaultAsync(m => m.PostID == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

       

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await usermanager.GetUserAsync(User);
            post.ID = user.Id;
            post.dateTime = DateTime.Now;
            
            context.Post.Add(post);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.ID }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await context.Post.SingleOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            context.Post.Remove(post);
            await context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return context.Post.Any(e => e.PostID == id);
        }
    }
}