using shopapp.webapi.Model;

namespace shopapp.webapi.IdentityServices.Abstract
{
    public interface IUserService
    {
        Task<bool> CreateAsync(RegisterModel model);
        Task<bool> ConfirmEmailAsync(string token,string userId);
        Task<bool> FargotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
        public string? Message { get; set; }
    }
}