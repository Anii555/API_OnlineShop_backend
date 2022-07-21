using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProductsLibrary.DB_Context;
using ProductsLibrary.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Login);

            if (userExists != null) //если пользователь существует
            {
                var check_pass = await _signInManager.CheckPasswordSignInAsync(userExists, model.Password, false);

                if (check_pass.Succeeded) //если пароль совпадает
                {
                    //авторизуем
                    return await Auth(userExists, model);
                }

                //если пароль не совпадает, то
                //просим ввести другой
                return Conflict("Неверный логин или пароль. Повторите попытку.");
            }

            //если не существует, то
            //добавляем нового пользователя
            var user = new User { UserName = model.Login };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);

                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Login);

            if (userExists != null) //если пользователь существует
            {
                var check_pass = await _signInManager.CheckPasswordSignInAsync(userExists, model.Password, false);

                if (check_pass.Succeeded) //если пароль совпадает
                {
                    //проверяем пароль и, в случае успеха, авторизовываем
                    return await Auth(userExists, model);
                }

                //если пароль не совпадает, то
                //просим ввести другой
                return Conflict("Неверный логин или пароль. Повторите попытку.");
            }

            return Unauthorized();
        }

        private async Task<IActionResult> Auth(User user, AuthModel model)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["JWT: ValidIssuer"],
            audience: _configuration["JWT: ValidAudience"],
            claims: authClaims,
            expires: DateTime.Now.AddHours(3)
            );

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                stringToken,
                expiration = token.ValidTo
            });
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