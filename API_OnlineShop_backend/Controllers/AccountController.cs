using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProductsLibrary.DB_Context;
using ProductsLibrary.Models;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Login);

            if (userExists != null)
            {
                return BadRequest("Пользователь с таким именем уже существует");
            }

            var user = new User { UserName = model.Login, SecurityStamp = Guid.NewGuid().ToString()};
            //добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);

                return Ok(result);
            }
            
            return BadRequest(model);
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(model, model.PasswordHash, false);
            
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Неправильный логин и (или) пароль");
            }
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            
            return Accepted();
        }
    }
}