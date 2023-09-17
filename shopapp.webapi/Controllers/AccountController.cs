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
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                return BadRequest(new ResponseObject{Message="User not found.",IsSuccsess=false});
            }

            var result = await signInManager!.PasswordSignInAsync(user,model.Password!,true,false);

            if(result.Succeeded)
            {
                return Ok(new ResponseObject{Message="Login successful.",IsSuccsess=true});
            }

            return BadRequest(new ResponseObject{Message="Wrong password.",IsSuccsess=false});
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager!.SignOutAsync();
            return Content("Logout successful.");
        }

        [HttpPost]
        [Route("confirmemail/{token}/{userId}")]
        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) && string.IsNullOrEmpty(userId))
            {
                return BadRequest("Token and userId not found");
            }

            var tokenlar = "123";
            var userlar = "123kkkkkkkkkkkk";

            await signInManager!.SignOutAsync();

            var url = $"http://localhost:5182/confirmemail/{tokenlar}/{userlar}";
            return Ok(url);
        }
    }
}