using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsLibrary;
using ProductsLibrary.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_OnlineShop_backend.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityUserController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        public IdentityUserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return Ok(users);
        }
        /////

        [HttpPost("create")]
        public async Task<IActionResult> Create(string email, string userName, string password)
        {
            User user = new User { Email = email, UserName = userName };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return Ok("Index");
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeEmailOrUsername([FromBody] IdentityUser model)
        {
            IdentityUser user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityResult result = new IdentityResult();
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                 result = await _userManager.DeleteAsync(user);
            }
            return Accepted(result);
        }
    }
}