using Microsoft.AspNetCore.Identity;
using shopapp.webapi.Helpers;
using shopapp.webapi.Identity;
using shopapp.webapi.IdentityServices.Abstract;
using shopapp.webapi.Model;
using shopapp.webui.EmailServices;

namespace shopapp.webapi.IdentityServices
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly IEmailSender? emailSender;
        public UserService(UserManager<ApplicationUser>? _userManager,IEmailSender? _emailSender)
        {
            userManager = _userManager;
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
            var validToken = TokenConverter.TokenToUrl(token);
            
            await emailSender!.SendEmailAsync(user.Email!,"Account confirmation.",$"Please click on the link to confirm your account.<p><a href='http://localhost:5197/api/Account/confirmemail/{validToken}&{user.Id}'>Click</a></p>");

            await userManager.AddToRoleAsync(user,"Customer");

            Message += "User created,Check your e-mail for confirmation.";
            return true;
        }

        public async Task<bool> ConfirmEmailAsync(string token,string userId)
        {

            if(string.IsNullOrEmpty(token) && string.IsNullOrEmpty(userId))
            {
                Message += "Token and UserId is required.";
                return false;
            }

            var user = await userManager!.FindByIdAsync(userId);

            if(user == null)
            {
                Message += "User not found.";
                return false;
            }

            var confirmToken = TokenConverter.UrlToToken(token);

            var result = await userManager!.ConfirmEmailAsync(user!,confirmToken);

            if(!result.Succeeded)
            {
                Message += "token error.";
                return false;
            }

            Message += "Confirmation successful.";
            return true;
        }
        public string? Message { get; set; }
    }


}