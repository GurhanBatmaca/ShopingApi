using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using shopapp.webapi.Identity;
using shopapp.webapi.Model;
using shopapp.webui.EmailServices;

namespace shopapp.webapi.IdentityServices
{
    public class UserService
    {
         readonly UserManager<ApplicationUser>? userManager;
         readonly SignInManager<ApplicationUser>? signInManager; 
         readonly RoleManager<IdentityRole>? roleManager; 
         readonly IEmailSender? emailSender;

        public UserService(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager,IEmailSender? _emailSender,RoleManager<IdentityRole>? _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            emailSender = _emailSender;
            roleManager = _roleManager;
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
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            
            await emailSender!.SendEmailAsync(user.Email!,"Üyelik Onayı.",$"Hesabınızı onaylamak için lütfen <a href='http://localhost:5197/api/Account/confirmemail/{validToken}&{user.Id}'>linke</a> tıklayınız");

            var result2 = await userManager.AddToRoleAsync(user,"Customer");

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

            var confirmToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));

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