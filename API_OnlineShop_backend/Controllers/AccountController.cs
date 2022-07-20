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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Login);
            var user = new User { UserName = model.Login, SecurityStamp = Guid.NewGuid().ToString() };
            var check_pass = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (userExists != null && check_pass.Succeeded)
            {
                return Conflict(await Login(new LoginModel { Login = model.Login, Password = model.Password })); //"Пользователь с таким именем уже существует"
            }

            //добавляем пользователя
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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (user != null && result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT: SecretKey"]));

                var token = new JwtSecurityToken(
                issuer: _configuration["JWT: ValidIssuer"],
                audience: _configuration["JWT: ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
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