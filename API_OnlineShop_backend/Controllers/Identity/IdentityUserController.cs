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
    public class IdentityUserController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public IdentityUserController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToListAsync();

            return Ok(users);
        }
    }
}