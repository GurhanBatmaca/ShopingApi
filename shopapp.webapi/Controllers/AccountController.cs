using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.webapi.Identity;
using shopapp.webapi.Model;

namespace shopapp.webapi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly SignInManager<ApplicationUser>? signInManager; 

        public AccountController(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Validation error.");
            }

            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await signInManager!.PasswordSignInAsync(user,model.Password!,true,false);

            if(result.Succeeded)
            {
                return Content("Login Succsessfuly.");
            }

            return BadRequest("Password is wrong.");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager!.SignOutAsync();
            return Content("Logout Succsessfuly.");
        }
    }
}