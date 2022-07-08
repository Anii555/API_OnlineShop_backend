using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_OnlineShop_backend.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    public class RoleManagerController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> manager)
        {
            _roleManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            return Ok(await _roleManager.Roles.ToListAsync());
        }
    }
}
