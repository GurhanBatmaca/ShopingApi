using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using shopapp.webapi.Identity;
using shopapp.webapi.IdentityServices.Abstract;
using shopapp.webapi.Model;

namespace shopapp.webapi.IdentityServices
{
    public class SignService: ISignService
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly IConfiguration? configuration;
        public SignService(UserManager<ApplicationUser>? _userManager,IConfiguration? _configuration)
        {
            userManager = _userManager;
            configuration = _configuration;
        }

        public string? Message { get; set; }
        public DateTime ExpireDate { get; set; }

        public async Task<bool> LoginAsync(LoginModel model)
        {
            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                Message += "No user found with this email address.";
                return false;
            }

            if(!await userManager.IsEmailConfirmedAsync(user))
            {
                Message += "Unconfirmed account.Please confirm your account with the link sent to your e-mail address.";
                return false;
            }

            var result = await userManager.CheckPasswordAsync(user,model.Password!);

            if(!result)
            {
                Message += "Password is incorrect.";
                return false;
            }

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Email",model.Email!),
                new Claim(ClaimTypes.NameIdentifier,user.Id)             
            };

            foreach (var role in roles)
            {
                claims.Add( new Claim(ClaimTypes.Role,role) );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration!["AuthSettings:Key"]!));

            var token = new JwtSecurityToken(
                issuer: configuration!["AuthSettings:Issuer"],
                audience: configuration!["AuthSettings:Audince"],
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            Message += stringToken;
            ExpireDate = token.ValidTo;

            return true;
        }

    }


}