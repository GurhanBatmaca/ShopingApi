using Microsoft.AspNetCore.Identity;
using shopapp.webapi.Identity;
using shopapp.webapi.Model;
using shopapp.webui.EmailServices;

namespace shopapp.webapi.IdentityServices
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly SignInManager<ApplicationUser>? signInManager; 
        private readonly IEmailSender? emailSender;

        public UserService(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager,IEmailSender? _emailSender)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            emailSender = _emailSender;
        }
        public async Task<bool> CreateAsync(RegisterModel model)
        {

            if(await userManager!.FindByNameAsync(model.UserName!) != null)
            {
                Message += "This username is already taken,Please choose a different one.";
                return false;
            }
            if(await userManager.FindByEmailAsync(model.Email!) != null)
            {
                Message += "A user has already been created with this e-mail address.";
                return false;
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user,model.Password!);

            if(!result.Succeeded)
            {
                Message += "Something went wrong.";
                return false;
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            // });http://localhost:5197/api/Account/confirmemail/ggggg/yyyyyy

            var url = $"http://localhost:5197/api/Account/confirmemail/{token}/{user.Id}";

            await emailSender!.SendEmailAsync(user.Email!,"Üyelik Onayı.","");


            Message += "User created,Check your e-mail for confirmation.";


            return true;
        }

        public string? Message { get; set; }
    }
}