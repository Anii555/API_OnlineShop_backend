using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_OnlineShop_backend.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagerController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;

        public UserManagerController(UserManager<IdentityUser> manager)
        {
            _userManager = manager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_userManager.Users.ToList());
        }
    }
}
