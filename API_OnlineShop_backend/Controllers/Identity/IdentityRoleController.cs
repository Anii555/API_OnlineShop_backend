using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_OnlineShop_backend.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityRoleController : ControllerBase
    {
        NorthwindContext _context;
        public IdentityRoleController(NorthwindContext db)
        {
            _context = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            _context.Add(new IdentityRole("simpleUser"));
            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
        }
    }
}
