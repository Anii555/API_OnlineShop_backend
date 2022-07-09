using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //return View(); 
            return Ok("Ну типа какой-то вывод");
        }

        [HttpPost("register/{model}")]
        public async Task<IActionResult> Register([FromBody] IdentityUser model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                //var result = await _userManager.CreateAsync(user, Input.Password);
                //if (result.Succeeded)
                //{
                //    // установка куки
                //    await _signInManager.SignInAsync(user, false);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    foreach (var error in result.Errors)
                //    {
                //        ModelState.AddModelError(string.Empty, error.Description);
                //    }
                //}
            }
            return Ok(model);
        }

        //[HttpGet("{returnUrl}")]
        //public IActionResult Login(string returnUrl = null)
        //{
        //    return Ok(new IdentityUser { ReturnUrl = returnUrl });
        //}

        [HttpPost("login/{model}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] IdentityUser model)
        {
            if (ModelState.IsValid)
            {
                //var result =
                //    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                //if (result.Succeeded)
                //{
                //    // проверяем, принадлежит ли URL приложению
                //    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                //    {
                //        return Redirect(model.ReturnUrl);
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                //}
            }
            return Ok(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}