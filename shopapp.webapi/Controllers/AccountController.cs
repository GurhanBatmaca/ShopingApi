using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.webapi.Identity;
using shopapp.webapi.IdentityServices;
using shopapp.webapi.IdentityServices.Abstract;
using shopapp.webapi.Model;

namespace shopapp.webapi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IUserService? userService;
        private readonly ISignService? signService;

        public AccountController(IUserService? _userService,ISignService? _signService)
        {
            userService = _userService;   
            signService = _signService;        
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if(await userService!.CreateAsync(model))
            {
                return Ok(new ResponseObject{ Message = userService.Message });
            }

            return BadRequest(new ResponseObject{ Message = userService.Message });
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if(!await signService!.LoginAsync(model))
            {
                return BadRequest(new ResponseObject{ Message = signService.Message });
            }

            return Ok(new ResponseObject{ Message = signService.Message, ExpireDate = signService.ExpireDate });
        }

        [HttpGet]
        [Route("confirmemail/{token}&{userId}")]
        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            if(await userService!.ConfirmEmailAsync(token,userId))
            {
                return Ok(new ResponseObject{ Message = userService.Message });               
            }

            return BadRequest(new ResponseObject{ Message = userService.Message });
            
        }
    }
}